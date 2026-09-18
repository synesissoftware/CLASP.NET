
// Created: 19th June 2017
// Updated: 5th May 2019

namespace Clasp
{
    using System;

    /// <summary>
    ///  [INTERNAL]
    /// </summary>
    [Flags]
    internal enum BoundFieldTypeCharacteristics : long
    {
        None                        =   0x0000000000000000,

        Class                       =   0x0000000000000010,
        Struct                      =   0x0000000000000020,

        String                      =   0x0000000000001000,
        Numeric                     =   0x0000000000002000,

        Integral                    =   0x0000000000010000,
        FloatingPoint               =   0x0000000000020000,

        Signed                      =   0x0000000000100000,

        Boolean                     =   0x0000000001000000,
        Character                   =   0x0000000002000000,

        SizeIs_8                    =   0x0000000100000000,
        SizeIs_16                   =   0x0000000200000000,
        SizeIs_32                   =   0x0000000400000000,
        SizeIs_64                   =   0x0000000800000000,
        SizeIs_128                  =   0x0000001000000000,
        SizeIs_256                  =   0x0000002000000000,
        SizeIs_Unknown              =   0x0000800000000000,
    }

    /// <summary>
    ///  [INTERNAL]
    /// </summary>
    internal enum BoundFieldType : long
    {
        Unknown                     =   (long)(BoundFieldTypeCharacteristics.None),
        Boolean                     =   (long)(BoundFieldTypeCharacteristics.Boolean),
        SignedInt32                 =   (long)(BoundFieldTypeCharacteristics.Integral | BoundFieldTypeCharacteristics.SizeIs_32 | BoundFieldTypeCharacteristics.Signed),
        UnsignedInt32               =   (long)(BoundFieldTypeCharacteristics.Integral | BoundFieldTypeCharacteristics.SizeIs_32),
        SignedInt64                 =   (long)(BoundFieldTypeCharacteristics.Integral | BoundFieldTypeCharacteristics.SizeIs_64 | BoundFieldTypeCharacteristics.Signed),
        UnsignedInt64               =   (long)(BoundFieldTypeCharacteristics.Integral | BoundFieldTypeCharacteristics.SizeIs_64),
        String                      =   (long)(BoundFieldTypeCharacteristics.String),
        Single                      =   (long)(BoundFieldTypeCharacteristics.FloatingPoint) | BoundFieldTypeCharacteristics.SizeIs_32 | BoundFieldTypeCharacteristics.Signed,
        Double                      =   (long)(BoundFieldTypeCharacteristics.FloatingPoint) | BoundFieldTypeCharacteristics.SizeIs_64 | BoundFieldTypeCharacteristics.Signed,
    }
}

