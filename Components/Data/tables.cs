using System;
public class Distance
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Timestamp { get; set; }
}

public class ObjectCount
{
    public int Id { get; set; }
    public int Count { get; set; }
    public DateTime Timestamp { get; set; }
}

public class BatteryPercentage
{
    public int Id { get; set; }
    public int Percentage { get; set; }
    public DateTime Timestamp { get; set; }
}