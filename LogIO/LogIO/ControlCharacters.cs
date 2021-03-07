﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIO
{
    enum ControlCharacters : byte
    {
        NUL = 0x00, SOH, STX, ETX, EOT, ENQ, ACK, BEL, BS, HT,
        LF, VT, FF, CR, SO, SI, DLE, DC1, DC2, DC3, DC4,
        NAK, SYN, ETB, CAN, EM, SUB, ESC, FS, GS, RS, US,
        DEL = 0x7f
    }
}