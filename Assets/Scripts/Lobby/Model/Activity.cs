using System;
using System.Collections.Generic;

[Serializable]
public class Activity
{
    public int id;
    public string name;
    public string description;
    public string startTime;
    public string endTime;
    public int maxParticipants;
    public string environmentId;
    public string status;

    // TODO: add students and moderators
}