﻿using System.Collections;
using System.Collections.Generic;
using Tests;
using UnityEngine;

public class SerializationManager
{
    public static void ServerWorldSerialize(BitBuffer buffer, Dictionary<int, Rigidbody> clientRigidbodies, int snapshotSeq, float serverTime) {
        buffer.PutByte((int) PacketType.Snapshot);
        buffer.PutInt(snapshotSeq);
        buffer.PutFloat(serverTime);
        buffer.PutByte(clientRigidbodies.Count);

        foreach (var clientRigidbodyPair in clientRigidbodies)
        {
            var clientId = clientRigidbodyPair.Key;
            var transform = clientRigidbodyPair.Value.transform;
            var position = transform.position;
            var rotation = transform.rotation;
            
            buffer.PutInt(clientId);
            buffer.PutFloat(position.x);
            buffer.PutFloat(position.y);
            buffer.PutFloat(position.z);
            buffer.PutFloat(rotation.w);
            buffer.PutFloat(rotation.x);
            buffer.PutFloat(rotation.y);
            buffer.PutFloat(rotation.z);
        }
    }

    public static List<Commands> ServerDeserializeInput(BitBuffer buffer)
    {
        List<Commands> totalCommands = new List<Commands>();
        
        while (buffer.HasRemaining())
        {
            int seq = buffer.GetInt();

            Commands commands = new Commands(
                seq,
                buffer.GetInt() > 0,
                buffer.GetInt() > 0,
                buffer.GetInt() > 0,
                buffer.GetInt() > 0,
                buffer.GetInt() > 0);

            totalCommands.Add(commands);
        }

        return totalCommands;
    }

    public static void ServerSerializeCommandAck(BitBuffer buffer, int commandSequence)
    {
        buffer.PutByte((int) PacketType.CommandAck);
        buffer.PutInt(commandSequence);
    }
}
