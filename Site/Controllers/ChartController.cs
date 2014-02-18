using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Site.Controllers
{
    public class ChartController : ApiController
    {
        private static readonly Lazy<Timer> _timer = new Lazy<Timer>(() => new Timer(TimerCallback, null, 0, 1000));
        private static readonly ConcurrentDictionary<int,StreamWriter> _streammessage = new ConcurrentDictionary<int,StreamWriter>();

        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            Timer t = _timer.Value;
            HttpResponseMessage response = request.CreateResponse();
            response.Content = new PushStreamContent((Stream stream, HttpContent headers, TransportContext context) => { OnStreamAvailable1(stream, headers, context); }, "text/event-stream");
            return response;
        }
        private static void TimerCallback(object state)
        {
            Random randNum = new Random();
            foreach (var data in _streammessage.OrderBy(k=>k.Key))
            {
                try
                {
                    data.Value.WriteLine("data:" + randNum.Next(30, 100) + "\n");
                    data.Value.Flush();
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    StreamWriter sw;
                    _streammessage.TryRemove(data.Key, out sw);
                }

            }
            //To set timer with random interval
            _timer.Value.Change(TimeSpan.FromMilliseconds(randNum.Next(1, 3) * 500), TimeSpan.FromMilliseconds(-1));

        }
        public static void OnStreamAvailable1(Stream stream, HttpContent headers, TransportContext context)
        {
            StreamWriter streamwriter = new StreamWriter(stream);
            //_streammessage.TryAdd(_streammessage.Count+1,streamwriter);

            StartWriting(streamwriter);
            
            
            
            
        }

        private static void StartWriting(StreamWriter streamwriter)
        {
            var text = "aa \n bb \n cc \n dd \n ee \n ff \n gg \n";
            var textInLines = text.Split('\n');
            //var tuple = new MyTuple() { Writer = streamwriter, Text = textInLines} ;
            //Tuple <StreamWriter,string[],int> tuple = new Tuple <StreamWriter,string[],int>(streamwriter,textInLines,0)
            //Timer t = new Timer(TimerCallback1, tuple, 0, 1000);
            //tuple.Time = t;

            foreach(var line in textInLines)
            {
                
                streamwriter.WriteLine("data:" + line + "\n");
                streamwriter.Flush();
                Thread.Sleep(1000);
            }
        }

        public class MyTuple
        {
            public StreamWriter Writer { get; set; }
            public string[] Text { get; set; }
            public int Index { get; set; }

            public Timer Time { get; set; }

            public MyTuple()
            {
                Index = 0;
            }

            
        }
        private static void TimerCallback1(object state)
        {
            //MyTextHelper.Instance.EmploymentHistory;

            Random randNum = new Random();
            var tuple = ((MyTuple)state);
            var data = tuple.Writer;

            if (tuple.Time != null)
            {
                if(tuple.Text.Length <= tuple.Index)
                {
                    tuple.Time.Change(0, System.Threading.Timeout.Infinite);
                    tuple.Time.Dispose();
                    tuple.Time = null;
                }
                else
                {
                    try
                    {
                        data.WriteLine(tuple.Text[tuple.Index++]);
                        data.Flush();
                    }
                    catch (Exception e)
                    {
                        StreamWriter sw;
                        tuple.Time.Change(0, System.Threading.Timeout.Infinite);
                        //_streammessage.TryRemove(data.Key, out sw);
                    }
                }
            }

                
            //To set timer with random interval
            //_timer.Value.Change(TimeSpan.FromMilliseconds(randNum.Next(1, 3) * 500), TimeSpan.FromMilliseconds(-1));

        }
    }
}
