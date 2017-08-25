﻿Public Enum Mode
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
    Keys = &H200
    KeysMax = &H2E6
    Collision = &H2E8
    Entrypoint = &H302
End Enum

Public Enum Keys As UInt16
    A = 4
    B = 10
    C = 16
    D = 22
    E = 28
    F = 34
    G = 40
    H = 46
    I = 52
    J = 58
    K = 64
    L = 70
    M = 76
    N = 82
    O = 88
    P = 94
    Q = 100
    R = 106
    S = 112
    T = 118
    U = 124
    V = 130
    W = 136
    X = 142
    Y = 148
    Z = 154
    D1 = 160
    D2 = 166
    D3 = 172
    D4 = 178
    D5 = 184
    D6 = 190
    D7 = 196
    D8 = 202
    D9 = 208
    D0 = 214
    EM = 220
    QM = 226
    Plus = 232
    Minus = 238
    Mult = 244
    Div = 250
    Dot = 256
    Colon = 262
    Right = 268
    Left = 274
    Up = 280
    Down = 286
    Equal = 292
    Greater = 298
    Lesser = 304
    Quote = 310
    BOpen = 316
    Pipe = 322
    BClose = 328
    Slash = 334
    Space = 340
    Comma = 346
End Enum