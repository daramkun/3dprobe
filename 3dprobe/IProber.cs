using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace _3dprobe
{
	public interface IProber : IDisposable
	{
		string Name { get; }
		IEnumerable Probes { get; }
	}

	public interface IProber<out T> : IProber where T : IProbe
	{
		new IEnumerable<T> Probes { get; }
	}

	public interface IProbe
	{
		public string DeviceName { get; }
	}

	public class SectionNameAttribute : Attribute
	{
		public string Name { get; }
		public SectionNameAttribute(string name) => Name = name;
	}

	public class ItemDescriptionAttribute : Attribute
	{
		public string ItemName { get; }
		public string ItemDescription { get; }

		public ItemDescriptionAttribute(string itemName, string itemDesc)
		{
			ItemName = itemName;
			ItemDescription = itemDesc;
		}
	}
}
