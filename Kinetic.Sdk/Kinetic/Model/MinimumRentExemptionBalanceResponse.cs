using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Kinetic.Sdk.Kinetic.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class MinimumRentExemptionBalanceResponse {
    /// <summary>
    /// Gets or Sets Lamports
    /// </summary>
    [DataMember(Name="lamports", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lamports")]
    public int Lamports { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MinimumRentExemptionBalanceResponse {\n");
      sb.Append("  Lamports: ").Append(Lamports).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
    }

}
}
