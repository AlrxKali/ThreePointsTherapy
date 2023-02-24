using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ThreePoints.Service
{
    class Service
    {
        HttpClient client;

        public Service() 
        {
            client = new HttpClient();
        }


    }
}
