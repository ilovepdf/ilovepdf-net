using System;
using iLovePdf.Model.Task;

namespace iLovePdf.Core
{
    /// <summary>
    ///     ILove Pdf API
    /// </summary>
    public class iLovePdfApi
    {
        /// <summary>
        ///     Make request to the ILovePDF Api
        /// </summary>
        /// <returns></returns>
        public T CreateTask<T>() where T : iLovePdfTask
        {
            var instance = (T) Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .SetKeys(_privateKey, _publicKey)
                .StartTask(instance.ToolName);

            var serverUrl = StringHelpers.Invariant($"{Settings.Host}{result.Server}");

            instance.SetServerTaskId(new Uri(serverUrl), result.TaskId);

            return instance;
        }

        /// <summary>
        ///     Make request to the ILovePDF Api
        /// </summary>
        /// <param name="encryptKey">encryption key for files. Only keys of sizes 16, 24 or 32 are supported.</param>
        /// <returns></returns>
        public T CreateTask<T>(String encryptKey) where T : iLovePdfTask
        {
            var instance = (T) Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .SetKeys(_privateKey, _publicKey)
                .SetEncryptKey(encryptKey)
                .StartTask(instance.ToolName);

            var serverUrl = StringHelpers.Invariant($"{Settings.Host}{result.Server}");

            instance.SetServerTaskId(new Uri(serverUrl), result.TaskId);

            return instance;
        }

        /// <summary>
        ///     Make request to the ILovePDF Api
        /// </summary>
        /// <param name="encryptKey">encryption key for files. Only keys of sizes 16, 24 or 32 are supported.</param>
        /// <param name="shouldUseBuiltInGenerator">create encrypt key, using build in generator</param>
        /// <returns></returns>
        public T CreateTask<T>(String encryptKey, Boolean shouldUseBuiltInGenerator) where T : iLovePdfTask
        {
            var instance = (T) Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .SetKeys(_privateKey, _publicKey)
                .SetEncryptKey(encryptKey, shouldUseBuiltInGenerator)
                .StartTask(instance.ToolName);

            var serverUrl = StringHelpers.Invariant($"{Settings.Host}{result.Server}");

            instance.SetServerTaskId(new Uri(serverUrl), result.TaskId);

            return instance;
        }

        internal static T ConnectTask<T>(iLovePdfTask parent) where T : iLovePdfTask
        {
            var instance = (T) Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .ConnectTask(parent.TaskId, instance.ToolName);

            instance.SetServerTaskId(parent.ServerUrl, result.TaskId);
            instance.AddFiles(result.Files);

            return instance;
        }

        #region Fields and constructor

        /// <summary>
        ///     Public key
        /// </summary>
        private readonly String _publicKey;

        /// <summary>
        ///     Private key
        /// </summary>
        private readonly String _privateKey;

        /// <summary>
        ///     Constructor for initialization private and public keys.
        /// </summary>
        /// <param name="publicKey">project public key</param>
        /// <param name="privateKey">project private key</param>
        public iLovePdfApi(String publicKey, String privateKey)
        {
            if (String.IsNullOrWhiteSpace(publicKey))
                throw new ArgumentOutOfRangeException(nameof(publicKey));

            if (String.IsNullOrWhiteSpace(privateKey))
                throw new ArgumentOutOfRangeException(nameof(privateKey));

            _privateKey = privateKey;
            _publicKey = publicKey;
        }

        #endregion
    }
}