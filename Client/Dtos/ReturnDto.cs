using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Dtos
{
    [Serializable()]
    public class ReturnDto
    {
        public bool Success { get; set; }
        public string Filename { get; set; }
        public byte[] Data { get; set; }
        public string Message { get; set; }

        public ReturnDto() { }

        public ReturnDto(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        public ReturnDto(bool success, string message, string filename, byte[] data)
        {
            this.Success = success;
            this.Filename = filename;
            this.Data = data;
            this.Message = message;
        }
    }
}
