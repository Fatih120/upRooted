// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.AvaloniaResourcesIndexReaderWriter
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Avalonia.Utilities;

public static class AvaloniaResourcesIndexReaderWriter
{
	public static List<AvaloniaResourcesIndexEntry> ReadIndex(Stream P_0)
	{
		using BinaryReader binaryReader = new BinaryReader(P_0, Encoding.UTF8, true);
		int num = binaryReader.ReadInt32();
		return num switch
		{
			1 => ReadXmlIndex(), 
			2 => ReadBinaryIndex(binaryReader), 
			_ => throw new Exception($"Unknown resources index format version {num}"), 
		};
	}

	private static List<AvaloniaResourcesIndexEntry> ReadXmlIndex()
	{
		throw new NotSupportedException("Found legacy resources index format: please recompile your XAML files");
	}

	private static List<AvaloniaResourcesIndexEntry> ReadBinaryIndex(BinaryReader P_0)
	{
		int num = P_0.ReadInt32();
		List<AvaloniaResourcesIndexEntry> list = new List<AvaloniaResourcesIndexEntry>(num);
		for (int i = 0; i < num; i++)
		{
			list.Add(new AvaloniaResourcesIndexEntry
			{
				Path = P_0.ReadString(),
				Offset = P_0.ReadInt32(),
				Size = P_0.ReadInt32()
			});
		}
		return list;
	}
}

