using Base;
using Base.PropertyBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ObjectLoader.Event
{
	[Serializable]
    public class Contest : ModelBase
    {
        public override string SqL => @"
SELECT JSONB_AGG(res)
FROM 
(
	SELECT
	JSONB_BUILD_OBJECT
	(
		'Id', ""Id"",
		'ModifiedByUserId', ""ModifiedByUserId"",
		'Version', ""Version"",
		'Name', ""Name"",
		'CreatedOn', ""CreatedOn"",
		'ModifiedOn', ""ModifiedOn"",
		'DateFrom', ""DateFrom"",
		'DateTo', ""DateTo"",
		'Place', ""Place"",
		'IsDeleted', ""IsDeleted"",
		'IsCurrentContest', ""IsCurrentContest""
	) ""MainObject""
	FROM ""Event"".""Contest""
	WHERE ""IsDeleted"" = FALSE
) res
";
        public override string SqlCount => "";
        public override string SqlInsert => @"INSERT INTO ""Event"".""Contest"" (""Name"", ""CreatedOn"", ""ModifiedOn"", ""DateFrom"", ""DateTo"", ""Place"", ""IsCurrentContest"") VALUES (@Name, @CreatedOn, @ModifiedOn, @DateFrom, @DateTo, @Place, @IsCurrentContest) RETURNING ""Id"";";
        public override string SqlUpdate => @"UPDATE ""Event"".""Contest"" SET ""Name"" = @Name, ""ModifiedOn"" = @ModifiedOn, ""DateFrom"" = @DateFrom, ""DateTo"" = @DateTo, ""Place"" = @Place, ""IsCurrentContest"" = @IsCurrentContest, ""IsDeleted"" = @IsDeleted WHERE ""Id"" = @Id RETURNING ""Id"";";
        public override string SqlHardDelete => "";

        private Guid? _id;
        [JsonProperty("Id")]
        public Guid? Id
		{
			get { return _id; }
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
        [JsonProperty("Version")]
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

		private string? _name;
        [JsonProperty("Name")]
        public string? Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
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

		private DateTime? _dateFrom;
        [JsonProperty("DateFrom")]
        public DateTime? DateFrom
		{
			get
			{
				return _dateFrom;
			}
			set
			{
				_dateFrom = value;
				OnPropertyChanged();
			}
		}

		private DateTime? _dateTo;
        [JsonProperty("DateTo")]
        public DateTime? DateTo
		{
			get
			{
				return _dateTo;
			}
			set
			{
				_dateTo = value;
				OnPropertyChanged();
			}
		}

		private string? _place;
        [JsonProperty("Place")]
        public string? Place
		{
			get
			{
				return _place;
			}
			set
			{
				_place = value;
				OnPropertyChanged();
			}
		}

		private bool? _isCurrentContest;
		public bool? IsCorrentContest
		{
			get
			{
				return _isCurrentContest;
			}
			set
			{
				_isCurrentContest = value;
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
