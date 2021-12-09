namespace Graffle.FlowSdk.Cryptography {
    public interface IMessageSigner {        
        byte[] Sign(byte[] bytes);
    }
}