using Base;
using Base.PropertyBase;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Model.Event
{
    public class Contest : ModelBase
    {
        public override string SqL => @"SELECT * FROM ""Event"".""Contest"" ";
        public override string SqlCount => "";
        public override string SqlInsert => @"INSERT INTO ""Event"".""Contest"" (""Name"", ""CreatedOn"", ""ModifiedOn"", ""DateFrom"", ""DateTo"") VALUES (@Name, @CreatedOn, @ModifiedOn, @DateFrom, @DateTo) RETURNING ""Id"";";
        public override string SqlUpdate => "";
        public override string SqlHardDelete => "";

        private Guid? _id;
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

		private string? _name;
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

    }
}
