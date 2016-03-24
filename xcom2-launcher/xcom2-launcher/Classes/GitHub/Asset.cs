using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM2Launcher.GitHub
{
    public class Asset
    {
              public string url {get;set;}
              public string id {get;set;}
              public string name {get;set;}
              public string label {get;set;}
              public User uploader {get;set;}
              public string content_type {get;set;}
              public string state {get;set;}
              public int size {get;set;}
              public int download_count {get;set;}
              public DateTime created_at {get;set;}
              public DateTime updated_at {get;set;}
              public string browser_download_url {get;set;}

    }
}
