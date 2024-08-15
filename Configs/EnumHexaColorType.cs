using System;

[Flags]
public enum EnumHexaColorType
{
	None = 0,
	Red = 1,
	Green = 2,
	Blue = 4,
	Yellow = 8,
	Purple = 0x10,
	Aqua = 0x20,
	White = 0x40,
	Black = 0x80
}
