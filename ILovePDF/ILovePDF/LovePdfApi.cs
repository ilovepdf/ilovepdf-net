using System;
using ILovePDF.Model.Task;

namespace ILovePDF
{
    /// <summary>
    /// ILove PDF API
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
        /// <param name="publickKey">project public key</param>
        /// <param name="privateKey">project private key</param>
        public LovePdfApi(string publickKey, string privateKey)
        {
            if (string.IsNullOrWhiteSpace(publickKey) || string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentOutOfRangeException("Public and private keys can't be empty.");
            }

            _privateKey = privateKey;
            _publicKey = publickKey;
        }

        #endregion

        /// <summary>
        /// Make request to the ILovePDF Api
        /// </summary>
        /// <param name="encryptKey">encryption key for files. Only keys of sizes 16, 24 or 32 are supported.</param>
        /// <param name="shouldUseBuiltInGenerator">create encrypt key, using build in generator</param>
        /// <returns></returns>
        public T CreateTask<T>(string encryptKey = "", bool shouldUseBuiltInGenerator = false) where T : LovePdfTask
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            var result = RequestHelper.Instance
                .SetKeys(_privateKey, _publicKey)
                .SetEncryptKey(encryptKey, shouldUseBuiltInGenerator)
                .StartTask(instance.GetToolName());

            instance.SetServerTaskId(result.Server, result.TaskId);


            return instance;
        }
    }
}

