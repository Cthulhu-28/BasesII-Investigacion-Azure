using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.ObjectModel;
using System.Data.Common;
/// <summary>
/// Summary description for Txn
/// </summary>
/// 
[DataContract]
public class Txn
{
    [DataMember]
    public DbTransaction id { get; set; }
 
}