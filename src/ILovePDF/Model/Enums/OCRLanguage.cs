using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace LovePdf.Model.Enums
{
    public enum OCRLanguage
    {
        // <summary>
        /// Inglés
        /// </summary>
        [EnumMember(Value = "eng")]
        Eng,

        /// <summary>
        /// Afrikáans
        /// </summary>
        [EnumMember(Value = "afr")]
        Afr,

        /// <summary>
        /// Amhárico
        /// </summary>
        [EnumMember(Value = "amh")]
        Amh,

        /// <summary>
        /// Árabe
        /// </summary>
        [EnumMember(Value = "ara")]
        Ara,

        /// <summary>
        /// Asamés
        /// </summary>
        [EnumMember(Value = "asm")]
        Asm,

        /// <summary>
        /// Azerí
        /// </summary>
        [EnumMember(Value = "aze")]
        Aze,

        /// <summary>
        /// Azerí (Cirílico)
        /// </summary>
        [EnumMember(Value = "aze_cyrl")]
        Aze_Cyrl,

        /// <summary>
        /// Bielorruso
        /// </summary>
        [EnumMember(Value = "bel")]
        Bel,

        /// <summary>
        /// Bengalí
        /// </summary>
        [EnumMember(Value = "ben")]
        Ben,

        /// <summary>
        /// Tibetano
        /// </summary>
        [EnumMember(Value = "bod")]
        Bod,

        /// <summary>
        /// Bosnio
        /// </summary>
        [EnumMember(Value = "bos")]
        Bos,

        /// <summary>
        /// Bretón
        /// </summary>
        [EnumMember(Value = "bre")]
        Bre,

        /// <summary>
        /// Búlgaro
        /// </summary>
        [EnumMember(Value = "bul")]
        Bul,

        /// <summary>
        /// Catalán
        /// </summary>
        [EnumMember(Value = "cat")]
        Cat,

        /// <summary>
        /// Cebuano
        /// </summary>
        [EnumMember(Value = "ceb")]
        Ceb,

        /// <summary>
        /// Checo
        /// </summary>
        [EnumMember(Value = "ces")]
        Ces,

        /// <summary>
        /// Chino Simplificado
        /// </summary>
        [EnumMember(Value = "chi_sim")]
        Chi_Sim,

        /// <summary>
        /// Chino Tradicional
        /// </summary>
        [EnumMember(Value = "chi_tra")]
        Chi_Tra,

        /// <summary>
        /// Cherokee
        /// </summary>
        [EnumMember(Value = "chr")]
        Chr,

        /// <summary>
        /// Corso
        /// </summary>
        [EnumMember(Value = "cos")]
        Cos,

        /// <summary>
        /// Galés
        /// </summary>
        [EnumMember(Value = "cym")]
        Cym,

        /// <summary>
        /// Danés
        /// </summary>
        [EnumMember(Value = "dan")]
        Dan,

        /// <summary>
        /// Danés (Fraktur)
        /// </summary>
        [EnumMember(Value = "dan_frak")]
        Dan_Frak,

        /// <summary>
        /// Alemán
        /// </summary>
        [EnumMember(Value = "deu")]
        Deu,

        /// <summary>
        /// Alemán (Fraktur)
        /// </summary>
        [EnumMember(Value = "deu_frak")]
        Deu_Frak,

        /// <summary>
        /// Dzongkha
        /// </summary>
        [EnumMember(Value = "dzo")]
        Dzo,

        /// <summary>
        /// Griego
        /// </summary>
        [EnumMember(Value = "ell")]
        Ell,

        /// <summary>
        /// Inglés Medio
        /// </summary>
        [EnumMember(Value = "enm")]
        Enm,

        /// <summary>
        /// Esperanto
        /// </summary>
        [EnumMember(Value = "epo")]
        Epo,

        /// <summary>
        /// Matemáticas
        /// </summary>
        [EnumMember(Value = "equ")]
        Equ,

        /// <summary>
        /// Estonio
        /// </summary>
        [EnumMember(Value = "est")]
        Est,

        /// <summary>
        /// Euskera
        /// </summary>
        [EnumMember(Value = "eus")]
        Eus,

        /// <summary>
        /// Feroés
        /// </summary>
        [EnumMember(Value = "fao")]
        Fao,

        /// <summary>
        /// Persa
        /// </summary>
        [EnumMember(Value = "fas")]
        Fas,

        /// <summary>
        /// Filipino
        /// </summary>
        [EnumMember(Value = "fil")]
        Fil,

        /// <summary>
        /// Finés
        /// </summary>
        [EnumMember(Value = "fin")]
        Fin,

        /// <summary>
        /// Francés
        /// </summary>
        [EnumMember(Value = "fra")]
        Fra,

        /// <summary>
        /// Franconio Antiguo
        /// </summary>
        [EnumMember(Value = "frk")]
        Frk,

        /// <summary>
        /// Francés Medio
        /// </summary>
        [EnumMember(Value = "frm")]
        Frm,

        /// <summary>
        /// Frisón
        /// </summary>
        [EnumMember(Value = "fry")]
        Fry,

        /// <summary>
        /// Gaélico Escocés
        /// </summary>
        [EnumMember(Value = "gla")]
        Gla,

        /// <summary>
        /// Irlandés
        /// </summary>
        [EnumMember(Value = "gle")]
        Gle,

        /// <summary>
        /// Gallego
        /// </summary>
        [EnumMember(Value = "glg")]
        Glg,

        /// <summary>
        /// Griego Clásico
        /// </summary>
        [EnumMember(Value = "grc")]
        Grc,

        /// <summary>
        /// Guyaratí
        /// </summary>
        [EnumMember(Value = "guj")]
        Guj,

        /// <summary>
        /// Criollo Haitiano
        /// </summary>
        [EnumMember(Value = "hat")]
        Hat,

        /// <summary>
        /// Hebreo
        /// </summary>
        [EnumMember(Value = "heb")]
        Heb,

        /// <summary>
        /// Hindi
        /// </summary>
        [EnumMember(Value = "hin")]
        Hin,

        /// <summary>
        /// Croata
        /// </summary>
        [EnumMember(Value = "hrv")]
        Hrv,

        /// <summary>
        /// Húngaro
        /// </summary>
        [EnumMember(Value = "hun")]
        Hun,

        /// <summary>
        /// Armenio
        /// </summary>
        [EnumMember(Value = "hye")]
        Hye,

        /// <summary>
        /// Inuktitut
        /// </summary>
        [EnumMember(Value = "iku")]
        Iku,

        /// <summary>
        /// Indonesio
        /// </summary>
        [EnumMember(Value = "ind")]
        Ind,

        /// <summary>
        /// Islandés
        /// </summary>
        [EnumMember(Value = "isl")]
        Isl,

        /// <summary>
        /// Italiano
        /// </summary>
        [EnumMember(Value = "ita")]
        Ita,

        /// <summary>
        /// Italiano Antiguo
        /// </summary>
        [EnumMember(Value = "ita_old")]
        Ita_Old,

        /// <summary>
        /// Javanés
        /// </summary>
        [EnumMember(Value = "jav")]
        Jav,

        /// <summary>
        /// Japonés
        /// </summary>
        [EnumMember(Value = "jpn")]
        Jpn,

        /// <summary>
        /// Canarés
        /// </summary>
        [EnumMember(Value = "kan")]
        Kan,

        /// <summary>
        /// Georgiano
        /// </summary>
        [EnumMember(Value = "kat")]
        Kat,

        /// <summary>
        /// Georgiano Antiguo
        /// </summary>
        [EnumMember(Value = "kat_old")]
        Kat_Old,

        /// <summary>
        /// Kazajo
        /// </summary>
        [EnumMember(Value = "kaz")]
        Kaz,

        /// <summary>
        /// Jemer
        /// </summary>
        [EnumMember(Value = "khm")]
        Khm,

        /// <summary>
        /// Kirguís
        /// </summary>
        [EnumMember(Value = "kir")]
        Kir,

        /// <summary>
        /// Kurdo Kurmanji
        /// </summary>
        [EnumMember(Value = "kmr")]
        Kmr,

        /// <summary>
        /// Coreano
        /// </summary>
        [EnumMember(Value = "kor")]
        Kor,

        /// <summary>
        /// Coreano Vertical
        /// </summary>
        [EnumMember(Value = "kor_vert")]
        Kor_Vert,

        /// <summary>
        /// Kurdo
        /// </summary>
        [EnumMember(Value = "kur")]
        Kur,

        /// <summary>
        /// Lao
        /// </summary>
        [EnumMember(Value = "lao")]
        Lao,

        /// <summary>
        /// Latín
        /// </summary>
        [EnumMember(Value = "lat")]
        Lat,

        /// <summary>
        /// Letón
        /// </summary>
        [EnumMember(Value = "lav")]
        Lav,

        /// <summary>
        /// Lituano
        /// </summary>
        [EnumMember(Value = "lit")]
        Lit,

        /// <summary>
        /// Luxemburgués
        /// </summary>
        [EnumMember(Value = "ltz")]
        Ltz,

        /// <summary>
        /// Malabar
        /// </summary>
        [EnumMember(Value = "mal")]
        Mal,

        /// <summary>
        /// Maratí
        /// </summary>
        [EnumMember(Value = "mar")]
        Mar,

        /// <summary>
        /// Macedonio
        /// </summary>
        [EnumMember(Value = "mkd")]
        Mkd,

        /// <summary>
        /// Maltés
        /// </summary>
        [EnumMember(Value = "mlt")]
        Mlt,

        /// <summary>
        /// Mongol
        /// </summary>
        [EnumMember(Value = "mon")]
        Mon,

        /// <summary>
        /// Maorí
        /// </summary>
        [EnumMember(Value = "mri")]
        Mri,

        /// <summary>
        /// Malayo
        /// </summary>
        [EnumMember(Value = "msa")]
        Msa,

        /// <summary>
        /// Birmano
        /// </summary>
        [EnumMember(Value = "mya")]
        Mya,

        /// <summary>
        /// Nepalí
        /// </summary>
        [EnumMember(Value = "nep")]
        Nep,

        /// <summary>
        /// Neerlandés
        /// </summary>
        [EnumMember(Value = "nld")]
        Nld,

        /// <summary>
        /// Noruego
        /// </summary>
        [EnumMember(Value = "nor")]
        Nor,

        /// <summary>
        /// Occitano
        /// </summary>
        [EnumMember(Value = "oci")]
        Oci,

        /// <summary>
        /// Oriya
        /// </summary>
        [EnumMember(Value = "ori")]
        Ori,

        /// <summary>
        /// Panyabí
        /// </summary>
        [EnumMember(Value = "pan")]
        Pan,

        /// <summary>
        /// Polaco
        /// </summary>
        [EnumMember(Value = "pol")]
        Pol,

        /// <summary>
        /// Portugués
        /// </summary>
        [EnumMember(Value = "por")]
        Por,

        /// <summary>
        /// Pashto
        /// </summary>
        [EnumMember(Value = "pus")]
        Pus,

        /// <summary>
        /// Quechua
        /// </summary>
        [EnumMember(Value = "que")]
        Que,

        /// <summary>
        /// Rumano
        /// </summary>
        [EnumMember(Value = "ron")]
        Ron,

        /// <summary>
        /// Ruso
        /// </summary>
        [EnumMember(Value = "rus")]
        Rus,

        /// <summary>
        /// Sánscrito
        /// </summary>
        [EnumMember(Value = "san")]
        San,

        /// <summary>
        /// Cingalés
        /// </summary>
        [EnumMember(Value = "sin")]
        Sin,

        /// <summary>
        /// Eslovaco
        /// </summary>
        [EnumMember(Value = "slk")]
        Slk,

        /// <summary>
        /// Eslovaco (Fraktur)
        /// </summary>
        [EnumMember(Value = "slk_frak")]
        Slk_Frak,

        /// <summary>
        /// Esloveno
        /// </summary>
        [EnumMember(Value = "slv")]
        Slv,

        /// <summary>
        /// Sindhi
        /// </summary>
        [EnumMember(Value = "snd")]
        Snd,

        /// <summary>
        /// Español
        /// </summary>
        [EnumMember(Value = "spa")]
        Spa,

        /// <summary>
        /// Español Antiguo
        /// </summary>
        [EnumMember(Value = "spa_old")]
        Spa_Old,

        /// <summary>
        /// Albanés
        /// </summary>
        [EnumMember(Value = "squ")]
        Squ,

        /// <summary>
        /// Serbio
        /// </summary>
        [EnumMember(Value = "srp")]
        Srp,

        /// <summary>
        /// Serbio (Latino)
        /// </summary>
        [EnumMember(Value = "srp_latn")]
        Srp_Latn,

        /// <summary>
        /// Sundanés
        /// </summary>
        [EnumMember(Value = "sun")]
        Sun,

        /// <summary>
        /// Suajili
        /// </summary>
        [EnumMember(Value = "swa")]
        Swa,

        /// <summary>
        /// Sueco
        /// </summary>
        [EnumMember(Value = "swe")]
        Swe,

        /// <summary>
        /// Siríaco
        /// </summary>
        [EnumMember(Value = "syr")]
        Syr,

        /// <summary>
        /// Tamil
        /// </summary>
        [EnumMember(Value = "tam")]
        Tam,

        /// <summary>
        /// Tártaro
        /// </summary>
        [EnumMember(Value = "tat")]
        Tat,

        /// <summary>
        /// Telugu
        /// </summary>
        [EnumMember(Value = "tel")]
        Tel,

        /// <summary>
        /// Tayiko
        /// </summary>
        [EnumMember(Value = "tgk")]
        Tgk,

        /// <summary>
        /// Tagalo
        /// </summary>
        [EnumMember(Value = "tgl")]
        Tgl,

        /// <summary>
        /// Tailandés
        /// </summary>
        [EnumMember(Value = "tha")]
        Tha,

        /// <summary>
        /// Tigriña
        /// </summary>
        [EnumMember(Value = "tir")]
        Tir,

        /// <summary>
        /// Tongano
        /// </summary>
        [EnumMember(Value = "ton")]
        Ton,

        /// <summary>
        /// Turco
        /// </summary>
        [EnumMember(Value = "tur")]
        Tur,

        /// <summary>
        /// Uigur
        /// </summary>
        [EnumMember(Value = "uig")]
        Uig,

        /// <summary>
        /// Ucraniano
        /// </summary>
        [EnumMember(Value = "ukr")]
        Ukr,

        /// <summary>
        /// Urdu
        /// </summary>
        [EnumMember(Value = "urd")]
        Urd,

        /// <summary>
        /// Uzbeko
        /// </summary>
        [EnumMember(Value = "uzb")]
        Uzb,

        /// <summary>
        /// Uzbeko (Cirílico)
        /// </summary>
        [EnumMember(Value = "uzb_cyrl")]
        Uzb_Cyrl,

        /// <summary>
        /// Vietnamita
        /// </summary>
        [EnumMember(Value = "vie")]
        Vie,

        /// <summary>
        /// Yídish
        /// </summary>
        [EnumMember(Value = "yid")]
        Yid,

        /// <summary>
        /// Yoruba
        /// </summary>
        [EnumMember(Value = "yor")]
        Yor
    }
    
}
