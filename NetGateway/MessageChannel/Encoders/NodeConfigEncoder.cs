﻿using System;
using System.Collections.Generic;
using HelloHome.NetGateway.MessageChannel.Domain.Commands;

namespace HelloHome.NetGateway.MessageChannel.Encoders
{
	public class NodeConfigEncoder : MessageEncoder<NodeConfigCommand>
	{
		readonly PinConfigEncoder _pinConfigEncoder;

		public NodeConfigEncoder (PinConfigEncoder pinConfigEncoder)
		{
			this._pinConfigEncoder = pinConfigEncoder;			
		}

		public override byte[] EncodeInternal (NodeConfigCommand message)
		{
			var bytes = new List<byte> ();
			bytes.Add (2 + (0 << 2));
		    bytes.AddRange(BitConverter.GetBytes(message.Signature));
			bytes.Add(message.NewRfAddress);
			bytes.Add(_pinConfigEncoder.EncodePins(message.Hal1Pin, message.Hal2Pin));
			bytes.Add(_pinConfigEncoder.EncodePins(message.DryPin));
			bytes.Add(_pinConfigEncoder.EncodePins(message.VInTriggerPin, message.VInMeasurePin));

		    var features = message.SiEnable ? 1 : 0;
		    features += (message.BmpEnable ? 1 : 0) << 1;
		    bytes.Add ((byte)features);

			return bytes.ToArray ();
		}
	}
}
