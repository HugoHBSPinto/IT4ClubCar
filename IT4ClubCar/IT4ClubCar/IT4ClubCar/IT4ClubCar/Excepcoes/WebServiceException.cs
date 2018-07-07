using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Excepcoes
{
    class WebServiceException : Exception
    {
        public WebServiceException() : base() { }
        public WebServiceException(string message) : base(message) { }
        public WebServiceException(string message, Exception e) : base(message, e) { }
    }
}
