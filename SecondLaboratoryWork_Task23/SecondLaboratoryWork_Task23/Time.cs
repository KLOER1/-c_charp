using System;

namespace SecondLaboratoryWork_Task23
{
    public class Time
    {
        private byte hours;
        private byte minutes;

        public byte Hours { get => hours; }
        public byte Minutes { get => minutes; }

        public Time(string hours, string minutes)
        {
            this.hours = byte.TryParse(hours, out this.hours) ? Convert.ToByte(hours) : (byte)0;
            this.minutes = byte.TryParse(minutes, out this.minutes) ? Convert.ToByte(minutes) : (byte)0;
            
            if (this.minutes >= 60)
            {
                this.hours += (byte)(this.minutes / 60);
                this.minutes %= 60;
            }
            if (this.hours >= 24) this.hours %= 24;
        }

        public override string ToString() => $"{hours}:{(this.minutes < 10 ? "0" : "")}{minutes}";

        public Time Subtraction(Time time)
        {
            if (this.Minutes < time.Minutes)
            {
                this.minutes = (byte)(60 + this.minutes - time.minutes);
                this.hours = (this.hours == 0) ? (byte)23 : --this.hours;
            }
            else this.minutes -= time.Minutes;

            this.hours = (this.hours < time.Hours) ? (byte)(24 + this.hours - time.hours) : (byte)(this.hours - time.hours);

            return this;
        }

        static public Time operator ++(Time time)
        {
            if (time.minutes == 59)
            {
                time.minutes = 0;
                if (time.hours == 23) time.hours = 0;
                else ++time.hours;
            }
            else ++time.minutes;
            return time;
        }

        static public Time operator --(Time time)
        {
            if (time.minutes == 0)
            {
                time.minutes = 59;
                if (time.hours == 0) time.hours = 23;
                else --time.hours;
            }
            else --time.minutes;
            return time;
        }

        static public implicit operator int(Time time) => time.hours * 60 + time.minutes;

        static public explicit operator bool(Time time) => time.hours != 0 && time.minutes != 0;

        static public bool operator >(Time FirstTime, Time SecondTime) => (int)FirstTime > (int)SecondTime;

        static public bool operator< (Time FirstTime, Time SecondTime) => (int)FirstTime < (int)SecondTime;
    }
}
