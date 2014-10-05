using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFreeGo.IntelligentControlPlatform.Modbus
{
    public enum FunEnum : byte
    {
        None = 0,
        RESET_MCU = 0x01,
        LED1_TOGLE = 0x11,
        LED2_TOGLE = 0x12,
        LED3_TOGLE = 0x13,
        LED4_TOGLE = 0x14,
        LED1_ON = 0x15,
        LED2_ON = 0x16,
        LED3_ON = 0x17,
        LED4_ON = 0x18,
        LED1_OFF = 0x19,
        LED2_OFF = 0x1A,
        LED3_OFF = 0x1B,
        LED4_OFF = 0x1C,
        LED_ALL_ON = 0x1D,
        LED_ALL_OFF = 0x1F,

        WRITE_EEPROM = 0x30,
        READ_EEPROM = 0x31,

        Error = 0xAA,
        GetCurrent = 0x51,

        PROTECT_RUN = 0x40,
        PROTECT_STOP = 0x41,

        
        SET_SUDUAN_PROTECT = 0x42,
        SET_YANSHI_PROTECT = 0x43,
    }
}
