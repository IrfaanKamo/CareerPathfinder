using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectPathfinder.Result.Utilities
{
    ///<summary>
    ///Serializes and Deserializes CareerPathfinderResult objects to and from a byte array
    ///</summary>
    public class ResultSerializer
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------
        ///<summary>
        ///Returns a byte array of the serialized CareerPathfinderResult
        ///</summary>
        public static byte[] SerializeResult(CareerPathfinderResult result)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, result);
                log.Info("Result object successfully serialized");

                return ms.GetBuffer();
            }
            catch (Exception ex)
            {
                log.Error("Result serialization failed: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------
        ///<summary>
        ///Gets the CareerPathfinderResult from the specified byte array
        ///</summary>
        public static CareerPathfinderResult DeserializeResult(byte[] result)
        {
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(result, 0, result.Length);
                ms.Position = 0;

                return bf.Deserialize(ms) as CareerPathfinderResult;
            }
            catch (Exception ex)
            {
                log.Error("Result deserialization failed: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------
    }
}