// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKManagedStreamDelegates
using System;
using SkiaSharp;

internal struct SKManagedStreamDelegates : IEquatable<SKManagedStreamDelegates>
{
	public SKManagedStreamReadProxyDelegate fRead;

	public SKManagedStreamPeekProxyDelegate fPeek;

	public SKManagedStreamIsAtEndProxyDelegate fIsAtEnd;

	public SKManagedStreamHasPositionProxyDelegate fHasPosition;

	public SKManagedStreamHasLengthProxyDelegate fHasLength;

	public SKManagedStreamRewindProxyDelegate fRewind;

	public SKManagedStreamGetPositionProxyDelegate fGetPosition;

	public SKManagedStreamSeekProxyDelegate fSeek;

	public SKManagedStreamMoveProxyDelegate fMove;

	public SKManagedStreamGetLengthProxyDelegate fGetLength;

	public SKManagedStreamDuplicateProxyDelegate fDuplicate;

	public SKManagedStreamForkProxyDelegate fFork;

	public SKManagedStreamDestroyProxyDelegate fDestroy;

	public readonly bool Equals(SKManagedStreamDelegates P_0)
	{
		if (fRead == P_0.fRead && fPeek == P_0.fPeek && fIsAtEnd == P_0.fIsAtEnd && fHasPosition == P_0.fHasPosition && fHasLength == P_0.fHasLength && fRewind == P_0.fRewind && fGetPosition == P_0.fGetPosition && fSeek == P_0.fSeek && fMove == P_0.fMove && fGetLength == P_0.fGetLength && fDuplicate == P_0.fDuplicate && fFork == P_0.fFork)
		{
			return fDestroy == P_0.fDestroy;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKManagedStreamDelegates sKManagedStreamDelegates)
		{
			return Equals(sKManagedStreamDelegates);
		}
		return false;
	}

	public static bool operator ==(SKManagedStreamDelegates P_0, SKManagedStreamDelegates P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKManagedStreamDelegates P_0, SKManagedStreamDelegates P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fRead);
		hashCode.Add(fPeek);
		hashCode.Add(fIsAtEnd);
		hashCode.Add(fHasPosition);
		hashCode.Add(fHasLength);
		hashCode.Add(fRewind);
		hashCode.Add(fGetPosition);
		hashCode.Add(fSeek);
		hashCode.Add(fMove);
		hashCode.Add(fGetLength);
		hashCode.Add(fDuplicate);
		hashCode.Add(fFork);
		hashCode.Add(fDestroy);
		return hashCode.ToHashCode();
	}
}

