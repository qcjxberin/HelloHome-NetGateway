﻿using System.Runtime.Serialization;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Commands
{
	[DataContract]
    public class NodeConfigCommand : OutgoingMessage
	{
		[DataMember]
	    public long signature { get; set; }

	    [DataMember]
	    public byte NewRfAddress { get; set; }

	    [DataMember]
	    public PinConfig Hal1Pin { get; set; }

	    [DataMember]
	    public PinConfig Hal2Pin { get; set; }

	    [DataMember]
	    public PinConfig DryPin { get; set; }

	    [DataMember]
	    public PinConfig vInTriggerPin { get; set; }

	    [DataMember]
	    public PinConfig vInMeasurePin { get; set; }

	    [DataMember]
	    public bool SiEnable { get; set; }

	    [DataMember]
	    public bool BmpEnable { get; set; }
	}


}
