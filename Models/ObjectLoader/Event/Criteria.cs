using Base;
using Base.PropertyBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectLoader.Event
{
    [Serializable]
    public class Criteria : ModelBase
    {
        public override string SqL => @"
SELECT JSONB_AGG(res)
FROM (
    SELECT 
    JSONB_BUILD_OBJECT
    (
        'Id', crt.""Id"", 
        'ModifiedByUserId', crt.""ModifiedByUserId"", 
        'CriteriaName', crt.""CriteriaName"", 
        'Sequence', crt.""Sequence"",
        'Percentage', crt.""Percentage"", 
        'CreatedOn', crt.""CreatedOn"", 
        'ModifiedOn', crt.""ModifiedOn"",
        'Contest', JSON_BUILD_OBJECT(
            'Id', conts.""Id"", 
            'ModifiedByUserId', conts.""ModifiedByUserId"",
            'Version', conts.""Version"", 
            'Name', conts.""Name"", 
            'CreatedOn', conts.""CreatedOn"", 
            'ModifiedOn', conts.""ModifiedOn"", 
            'DateFrom', conts.""DateFrom"", 
            'DateTo', conts.""DateTo"", 
            'Place', conts.""Place""
        )
    ) ""MainObject""
    FROM ""Event"".""Criterias"" crt
    LEFT OUTER JOIN ""Event"".""Contest"" conts ON conts.""Id"" = crt.""ContestId""
    WHERE crt.""IsDeleted"" = FALSE
) res
";
        public override string SqlCount => "";
        public override string SqlInsert => @"INSERT INTO ""Event"".""Criterias"" (""ModifiedByUserId"", ""Version"", ""ContestId"", ""CriteriaName"", ""Sequence"", ""Percentage"", ""CreatedOn"", ""ModifiedOn"")VALUES(@ModifiedByUserId, @Version, @ContestId, @CriteriaName, @Sequence, @Percentage, @CreatedOn, @ModifiedOn) RETURNING ""Id""";
        public override string SqlUpdate => "";
        public override string SqlHardDelete => "";

        private Guid? _id;
        [JsonProperty("Id")]
        public Guid? Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private Guid? _modifiedByUserId;
        [JsonProperty("ModifiedByUserId")]
        public Guid? ModifiedByUserId
        {
            get
            {
                return _modifiedByUserId;
            }
            set
            {
                _modifiedByUserId = value;
                OnPropertyChanged();
            }
        }

        private int? _version;
        public int? Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
                OnPropertyChanged();
            }
        }

        private string? _criteriaName;
        [JsonProperty("CriteriaName")]
        public string? CriteriaName
        {
            get
            {
                return _criteriaName;
            }
            set
            {
                _criteriaName = value;
                OnPropertyChanged();
            }
        }

        private int? _sequence;
        [JsonProperty("Sequence")]
        public int? Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
                OnPropertyChanged();
            }
        }

        private double? _percentage;
        [JsonProperty("Percentage")]
        public double? Percentage
        {
            get
            {
                return _percentage;
            }
            set
            {
                _percentage = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _createdOn;
        [JsonProperty("CreatedOn")]
        public DateTime? CreatedOn
        {
            get
            {
                return _createdOn;
            }
            set
            {
                _createdOn = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _modifiedOn;
        [JsonProperty("ModifiedOn")]
        public DateTime? ModifiedOn
        {
            get
            {
                return _modifiedOn;
            }
            set
            {
                _modifiedOn = value;
                OnPropertyChanged();
            }
        }

        private Contest? _contest;
        [JsonProperty("Contest")]
        public Contest? Contest
        {
            get
            {
                if (_contest == null)
                    _contest = new Contest();
                return _contest;
            }
            set
            {
                _contest = value;
                OnPropertyChanged();
            }
        }

        private bool? _isDeleted;
        public bool? IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                _isDeleted = value;
                OnPropertyChanged();
            }
        }
    }
}
