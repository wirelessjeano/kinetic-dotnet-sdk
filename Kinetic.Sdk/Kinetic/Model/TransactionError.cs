using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using TransactionErrorType = System.String;

namespace Kinetic.Sdk.Kinetic.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class TransactionError {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or Sets Logs
    /// </summary>
    [DataMember(Name="logs", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "logs")]
    public List<string> Logs { get; set; }

    /// <summary>
    /// Gets or Sets Message
    /// </summary>
    [DataMember(Name="message", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="type", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "type")]
    public TransactionErrorType Type { get; set; }

    /// <summary>
    /// Gets or Sets Instruction
    /// </summary>
    [DataMember(Name="instruction", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "instruction")]
    public int Instruction { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class TransactionError {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Logs: ").Append(Logs).Append("\n");
      sb.Append("  Message: ").Append(Message).Append("\n");
      sb.Append("  Type: ").Append(Type).Append("\n");
      sb.Append("  Instruction: ").Append(Instruction).Append("\n");
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
