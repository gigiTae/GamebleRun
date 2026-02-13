using System;


// [정적 클래스]: C++의 전역 함수 모음집 역할 (객체 생성 불가)
public static class GuidExtensions
{
    // Guid -> SerializableGuid 변환
    // [확장 메서드]: 'this' 문법을 통해 Guid 클래스에 원래 있던 메서드처럼 사용 가능하게 함
    // 사용 예: myGuid.ToSerializableGuid();
    public static SerializableGuid ToSerializableGuid(this Guid systemGuid)
    {
        byte[] bytes = systemGuid.ToByteArray();
        return new SerializableGuid(
            BitConverter.ToUInt32(bytes, 0),
            BitConverter.ToUInt32(bytes, 4),
            BitConverter.ToUInt32(bytes, 8),
            BitConverter.ToUInt32(bytes, 12)
        );
    }

    // Guid <- SerializableGuid 변환
    public static Guid ToSystemGuid(this SerializableGuid serializableGuid)
    {
        byte[] bytes = new byte[16];
        Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part1), 0, bytes, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part2), 0, bytes, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part3), 0, bytes, 8, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part4), 0, bytes, 12, 4);
        return new Guid(bytes);
    }
}