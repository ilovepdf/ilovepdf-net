# Documentación Técnica – Arquitectura del SDK iLovePdf .NET

## 1. Propósito

Este documento describe la arquitectura interna del SDK cliente `iLovePdf.Net`, una librería que envuelve la API REST de iLovePdf para permitir a aplicaciones .NET ejecutar operaciones sobre PDFs (comprimir, combinar, firmar, marca de agua, etc.) sin interactuar directamente con HTTP.

Repositorio: `ilovepdf-net` — Proyectos multi-target: `.NET Standard 1.5`, `.NET Standard 2.0`, `.NET Framework 4.5`, `.NET Framework 4.6`, `.NET 6`.

---

## 2. Vista general de la arquitectura

La librería está organizada en capas, desde el consumidor final hasta el transporte HTTP con la API remota de iLovePdf. Ver diagrama adjunto (`architecture-diagram.png` / `.pdf`).

| Capa | Componentes principales | Responsabilidad |
|---|---|---|
| 1. Aplicación Cliente | Proyectos `samples/*` | Código consumidor que usa el SDK |
| 2. Fachada Pública (SDK API) | `iLovePdfApi` | Punto de entrada único: creación y conexión de tareas |
| 3. Modelo de Tareas | `iLovePdfTask` (abstracta) + tareas concretas (`CompressTask`, `MergeTask`, `WaterMarkTask`, `SignTask`, etc.) | Flujo común de subida, procesamiento y descarga de archivos |
| 4. Modelo de Parámetros | `BaseParams` y derivados (`WaterMarkParams`, `SignParams`...) | Representan la configuración específica de cada herramienta |
| 5. Infraestructura HTTP | `RequestHelper`, `RequestHelperSignature`, `BaseTaskRequest`, `InitialValueHelper` | Construcción de requests HTTP, autenticación JWT, chunking, serialización |
| 6. Transporte / Externo | `HttpClient`, API REST iLovePdf | Comunicación real sobre HTTPS |

---

## 3. Patrones de diseño identificados

| Patrón | Dónde se aplica | Justificación |
|---|---|---|
| **Facade** | `iLovePdfApi` | Expone una API simple (`CreateTask<T>()`) que oculta la complejidad de autenticación, inicio de tarea y manejo de servidor. |
| **Template Method / Herencia** | `iLovePdfTask` (clase abstracta) + tareas concretas | La clase base define el flujo común (`AddFile`, `Process`, `DownloadFile`, `DeleteTask`); cada subclase especializa `ToolName` y métodos propios (p. ej. `UploadWatermark`). |
| **Singleton** | `RequestHelper.Instance` | Centraliza el `HttpClient` y el estado de autenticación (JWT) para evitar múltiples clientes HTTP y repetir lógica de tokens. |
| **Builder / Fluent Interface** | `RequestHelper.SetKeys(...).SetEncryptKey(...).StartTask(...)` | Permite configurar el helper de forma encadenada antes de ejecutar la acción. |
| **Strategy (implícito)** | Modelos `*Params` con variantes (`WatermarkModeText` vs `WatermarkModeImage`, `SplitModeFixedRanges` vs `SplitModeRanges`) | Cada "modo" encapsula un comportamiento/parametrización distinta para la misma operación. |
| **Partial Classes** | `RequestHelper` / `RequestHelperSignature` | Separa responsabilidades (tareas genéricas vs. endpoints de firma) manteniendo un mismo tipo lógico. |
| **DTO / Response Models** | `UploadTaskResponse`, `ExecuteTaskResponse`, `StatusTaskResponse`, `SignatureResponse` | Representan contratos de datos de la API REST, deserializados con Newtonsoft.Json. |

---

## 4. Tecnologías y dependencias clave

- **Lenguaje/Runtime**: C# 9.0, multi-targeting (`netstandard1.5`, `netstandard2.0`, `net45`, `net46`, `net6.0`) definido vía SDK-style `.csproj`.
- **HTTP**: `System.Net.Http.HttpClient` (con referencia explícita a `System.Net.Http` en `net45`/`net46`).
- **Autenticación**: JWT (JSON Web Token) generado y validado con la librería `jose-jwt`, firmado con `HS256` usando la clave privada del proyecto.
- **Serialización**: `Newtonsoft.Json` para (de)serializar requests/responses y parsear errores (`JObject.Parse`).
- **Empaquetado**: Generación automática de paquete NuGet (`GeneratePackageOnBuild`), símbolos `.snupkg`, firma fuerte con `iLovePdf.snk` (`AssemblyOriginatorKeyFile`).
- **Testing**: MSTest (`Microsoft.VisualStudio.TestTools.UnitTesting`), con una clase base `BaseTest` que abstrae ejecución de tareas variando combinaciones (chunking, descarga como bytes, cifrado).
- **CI/CD**: GitHub Actions (build multi-TFM, publicación a NuGet).

---

## 5. Flujo de ejecución (ciclo de vida de una request)

1. **Inicialización**: el consumidor crea `new iLovePdfApi(publicKey, privateKey)`.
2. **Creación de tarea**: `CreateTask<T>()` instancia la tarea vía reflexión (`Activator.CreateInstance`), configura claves en `RequestHelper.Instance` y llama a `StartTask` (endpoint `/start/{tool}`), obteniendo `serverUrl` y `taskId`.
3. **Carga de archivos**: `AddFile(...)` soporta tres orígenes (ruta local, `byte[]`, `Uri` remota) y dos modos de subida (directo o por chunks vía `UploadFileByChunk`, útil para archivos grandes, con tamaño de chunk configurado en `Settings.MaxBytesPerChunk` = 2 MB).
4. **Configuración de parámetros**: se construye un objeto `*Params` específico de la herramienta; para operaciones complejas (Sign, Edit, WaterMark multi-elemento) se usan colecciones de "elementos" anidados.
5. **Ejecución**: `Process(parameters)` invoca `RequestHelper.ExecuteTask`, que serializa parámetros y archivos como `multipart/form-data` (usando `InitialValueHelper` para aplanar objetos anidados en claves tipo `elements[0][...]`).
6. **Autenticación por request**: cada llamada añade un header `Authorization: Bearer <JWT>`, regenerando el token si expiró (`IsExpiredJwt`).
7. **Polling de estado**: el consumidor (o `BaseTest` en pruebas) consulta `CheckTaskStatus` hasta `TaskSuccess` o error.
8. **Descarga**: `DownloadFile` o `DownloadFileAsByteArrayAsync` obtiene el resultado (archivo único o `.zip` si son múltiples).
9. **Limpieza**: `DeleteTask` / `DeleteFile` opcionalmente liberan recursos en el servidor.
10. **Encadenamiento de herramientas**: `Next<T>()` permite reutilizar los archivos resultantes de una tarea como entrada de otra sin volver a subirlos (`ConnectTask`).

---

## 6. Manejo de errores

`RequestHelper.ParseRequestErrors` traduce códigos HTTP y el campo `error.type` de la respuesta JSON en excepciones tipadas específicas del dominio:

- `ProcessingException`
- `UploadException`
- `DownloadException`
- `AuthenticationException`
- `NotFoundException`
- `TooManyRequestsException`
- `ServerErrorException`
- `SignStartException`
- `SignatureException`

Esto permite a los consumidores capturar errores específicos sin parsear JSON manualmente.

---

## 7. Consideraciones de compatibilidad multi-target

- Directivas de compilación condicional (`#if NETSTANDARD1_5`) adaptan APIs no disponibles en runtimes antiguos (ej. `Assembly.GetExecutingAssembly()` vs `TypeInfo`).
- Referencias condicionales en `.csproj` (`System.Net.Http` solo para `net45`/`net46`).
- **Implicación en CI**: ejecutar pruebas de `net46` requiere un entorno Windows (o `mono` en Linux), a diferencia de `net6.0`/`netstandard2.0` que corren nativamente en cualquier runner con SDK de `dotnet`.

---

## 8. Puntos de extensión

- Nuevas herramientas de PDF se agregan creando: una subclase de `iLovePdfTask`, un `*Params` correspondiente, y (si aplica) `*ModeX` para variantes de comportamiento — sin modificar el núcleo de `RequestHelper`.
- El uso de `BaseExtraUploadParams` permite adjuntar parámetros opcionales en la subida de archivos sin romper firmas existentes.

---

## 9. Nuevas herramientas incorporadas

Siguiendo el patrón de extensión descrito en la sección anterior, se incorporaron las siguientes herramientas al SDK. Cada una sigue el mismo flujo común (crear tarea → subir archivo → procesar con parámetros → descargar), heredando de `iLovePdfTask` y definiendo su propio `*Params` cuando aplica.

| Herramienta | Endpoint / ToolName | Clase de Tarea | Parámetros |
|---|---|---|---|
| PDF a Markdown | `pdfmarkdown` | `PdfMarkdownTask` | Ninguno (`PdfMarkdownParams` vacío) |
| Split Smart | `splitsmart` | `SplitSmartTask` | `prompt` (string): instrucciones en lenguaje natural para dividir el PDF |
| Translate | `translate` | `TranslateTask` | `language_output` (string): idioma destino (ej. `eng`, `spa`)<br>`translate_mode` (string): `layout` o `text` |
| Forms Detect | `formsdetect` | `FormsDetectTask` | Ninguno (`FormsDetectParams` vacío) |
| Summarize | `summarize` | `SummarizeTask` | `language` (string): idioma destino del resumen (ej. `eng`, `spa`)<br>`output_format` (string): `pdf` o `md` |

Para cada herramienta se generaron además ejemplos de uso en `samples/` (p. ej. `PdfMarkdownBasic.cs`, `SplitSmartBasic.cs`, `TranslateBasic.cs`, `FormsDetectBasic.cs`, `SummarizeBasic.cs`) y pruebas unitarias en `tests/UnitTests/` siguiendo el mismo patrón que `CompressTests` (casos de credenciales inválidas y de procesamiento exitoso con parámetros por defecto).

---

## 10. Referencias de archivos clave

| Archivo | Rol |
|---|---|
| `src/ILovePDF/Core/iLovePdfApi.cs` | Fachada pública de entrada |
| `src/ILovePDF/Model/Task/iLovePdfTask.cs` | Clase base de todas las tareas |
| `src/ILovePDF/Core/RequestHelper.cs` | Motor HTTP, JWT, chunking, manejo de errores |
| `src/ILovePDF/Core/RequestHelperSignature.cs` | Endpoints específicos de firma digital |
| `src/ILovePDF/Core/BaseTaskRequest.cs` | Contenedor de form-data para requests |
| `src/ILovePDF/iLovePDF.csproj` | Configuración de multi-targeting y empaquetado NuGet |
| `src/ILovePDF/Model/Task/PdfMarkdownTask.cs` | Tarea: conversión de PDF a Markdown |
| `src/ILovePDF/Model/Task/SplitSmartTask.cs` | Tarea: división inteligente de PDF vía prompt |
| `src/ILovePDF/Model/Task/TranslateTask.cs` | Tarea: traducción de documentos PDF |
| `src/ILovePDF/Model/Task/FormsDetectTask.cs` | Tarea: detección de campos de formulario |
| `src/ILovePDF/Model/Task/SummarizeTask.cs` | Tarea: resumen de documentos PDF |

---

*Documento generado a partir del análisis del código fuente del repositorio `ilovepdf-net`.*
