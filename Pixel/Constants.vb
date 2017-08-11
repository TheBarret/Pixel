Public Enum Mode
    Scan
    Write
End Enum
Public Enum Locations As UInt16
    Pointer = &H0
    Overflow = &H2
    Fonts = &H4
    StackPtr = &H160
    Stack = &H162
    StackMax = &H1C5
    AddressPtr = &H1C7
    Address = &H1C9
    AddressMax = &H1FE
    Entrypoint = &H200
End Enum
