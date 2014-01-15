namespace TD1990.Libs.TDLyutil.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Base view model class. Implements INotifyPropertyChanged interface.
    /// Declares NotifyPropertyChanged methods.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        // todo create IBaseViewModel
        public virtual void Init()
        {
        }

        /// <summary>
        /// Property changed event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise Property changed event handler for the property.
        /// NotifyPropertyChanged(() => this.Property1);
        /// </summary>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="propertyExpression"></param>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("Invalid use of RaisePropertyChanged", "propertyExpression");
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("Invalid use of RaisePropertyChanged", "propertyExpression");

            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyInfo.Name));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        // protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
