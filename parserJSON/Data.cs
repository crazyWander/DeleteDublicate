namespace parserJSON;

public class Data
{
    public int cmd { get; set; }
    public int hdr1 { get; set; }
    public int hdr2 { get; set; }
    public int hdr3 { get; set; }
    public int hdr4 { get; set; }
    public int flags { get; set; }
    public int steps { get; set; }
    public int max_hr { get; set; }
    public int min_hr { get; set; }
    public string sensor_id { get; set; }
    public int rec_id { get; set; }
    public string add_date { get; set; }
    public int distance { get; set; }
    public int energy_in { get; set; }
    public int precision { get; set; }
    public int reserved1 { get; set; }
    public int reserved2 { get; set; }
    public int reserved3 { get; set; }
    public int timestamp { get; set; }
    public int energy_out { get; set; }
    public int heart_rate { get; set; }
    public int utc_offset { get; set; }
    public int first_rec_id { get; set; }
    public int stress_level { get; set; }
    public int battery_level { get; set; }
    public int activity_water_loss { get; set; }
    public double metabolic_water_loss { get; set; }
}

class DataList
{
    public List<Data> dataList = new List<Data>();
}