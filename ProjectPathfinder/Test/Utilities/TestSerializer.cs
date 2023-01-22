using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectPathfinder.Test.Utilities
{
    ///<summary>
    ///Serializes and Deserializes CareerPathfinderTest objects to and from a byte array
    ///</summary>
    public static class TestSerializer
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------
        ///<summary>
        ///Returns a byte array of the serialized CareerPathfinderTest
        ///</summary>
        public static byte[] SerializeTest(CareerPathfinderTest test)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, test);

                return ms.GetBuffer();
            }
            catch(Exception ex)
            {
                log.Error("Test serialization failed: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------
        ///<summary>
        ///Gets the CareerPathfinderTest from the specified byte array
        ///</summary>
        public static CareerPathfinderTest DeserializeTest(byte[] test)
        {
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(test, 0, test.Length);
                ms.Position = 0;

                return bf.Deserialize(ms) as CareerPathfinderTest;
            }
            catch (Exception ex)
            {
                log.Error("Test deserialization failed: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------
    }
}