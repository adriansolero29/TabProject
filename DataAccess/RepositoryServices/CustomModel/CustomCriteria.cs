using Base;
using Base.PropertyBase;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices.CustomModel
{
    public class CustomCriteria : PropChanged
    {
        private Criteria? _criteriaInfo;
        public Criteria? CriteriaInfo
        {
            get
            {
                if (_criteriaInfo == null)
                    _criteriaInfo = new Criteria();
                return _criteriaInfo;
            }
            set 
            {
                _criteriaInfo = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Criterion>? _criterionList;
        public ObservableCollection<Criterion>? CriterionList
        {
            get
            {
                if (_criterionList == null)
                    _criterionList = new ObservableCollection<Criterion>();
                return _criterionList;
            }
            set 
            { 
                _criterionList = value;
                OnPropertyChanged();
            }
        }
    }
}
