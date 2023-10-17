using System;

[Serializable]
public class ActivityAction {
    public int activityId;
    public int userId;
    public string action;
    public string timestamp;
    public int amountParticipants;
}
