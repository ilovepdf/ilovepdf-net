using System;
using LovePdf.Model.Task;

namespace LovePdf.Core
{
    /// <summary>
    /// ILove Pdf API
    /// </summary>
    public class LovePdfApi
    {
        #region Fields and constructor

        /// <summary>
        /// Public key
        /// </summary>
        private readonly string _publicKey;

        /// <summary>
        /// Private key
        /// </summary>
        private readonly string _privateKey;

        /// <summary>
        /// Constructor for initialization private and public keys.
        /// </summary>
        /// <param name="publicKey">project public key</param>
        /// <param name="privateKey">project private key</param>
        public LovePdfApi(string publicKey, string privateKey)
        {
            if (string.IsNullOrWhiteSpace(publicKey))
                throw new ArgumentOutOfRangeException(nameof(publicKey));

            if (string.IsNullOrWhiteSpace(privateKey))
                throw new ArgumentOutOfRangeException(nameof(privateKey));

            _privateKey = privateKey;
            _publicKey = publicKey;
        }

        #endregion

        /// <summary>
        /// Make request to the ILovePDF Api
        /// </summary>
        /// <returns></returns>
        public T CreateTask<T>() where T : LovePdfTask
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .SetKeys(_privateKey, _publicKey)
                .StartTask(instance.ToolName);

            var serverUrl = StringHelpers.Invariant($"{Settings.Host}{result.Server}");

            instance.SetServerTaskId(new Uri(serverUrl), result.TaskId);

            return instance;
        }

        /// <summary>
        /// Make request to the ILovePDF Api
        /// </summary>
        /// <param name="encryptKey">encryption key for files. Only keys of sizes 16, 24 or 32 are supported.</param>
        /// <returns></returns>
        public T CreateTask<T>(string encryptKey) where T : LovePdfTask
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .SetKeys(_privateKey, _publicKey)
                .SetEncryptKey(encryptKey)
                .StartTask(instance.ToolName);

            var serverUrl = StringHelpers.Invariant($"{Settings.Host}{result.Server}");

            instance.SetServerTaskId(new Uri(serverUrl), result.TaskId);


            return instance;
        }

        /// <summary>
        /// Make request to the ILovePDF Api
        /// </summary>
        /// <param name="encryptKey">encryption key for files. Only keys of sizes 16, 24 or 32 are supported.</param>
        /// <param name="shouldUseBuiltInGenerator">create encrypt key, using build in generator</param>
        /// <returns></returns>
        public T CreateTask<T>(string encryptKey, bool shouldUseBuiltInGenerator) where T : LovePdfTask
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .SetKeys(_privateKey, _publicKey)
                .SetEncryptKey(encryptKey, shouldUseBuiltInGenerator)
                .StartTask(instance.ToolName);

            var serverUrl = StringHelpers.Invariant($"{Settings.Host}{result.Server}");

            instance.SetServerTaskId(new Uri(serverUrl), result.TaskId);


            return instance;
        }
    }
}

