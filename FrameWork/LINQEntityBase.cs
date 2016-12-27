// Type: Framework.LINQEntityBase
// Assembly: Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 08937EDF-77C9-44A8-AAE6-D7084B72CFF9
// Assembly location: D:\Working Directory\Boss_Citrus\References\Framework.dll

using FrameWork;                 
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Framework
{
    [DataContract(IsReference = true)]
    [KnownType("GetKnownTypes")]
    public abstract class LINQEntityBase : IDataErrorInfo
    {
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> _cacheAssociationProperties;
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> _cacheAssociationFKProperties;
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> _cacheDBGeneratedProperties;
        private bool _isSyncronisingWithDB;
        private bool _isRoot;
        private EntityState _entityState;
        private bool _isKeepOriginal;
        private string _entityGUID;
        private LINQEntityBase.EntityTree _entityTree;
        private List<LINQEntityBase> _changeTrackingReferences;
        private LINQEntityBase _originalEntityValue;
        private LINQEntityBase _originalEntityValueTemp;

        [DataMember(Order = 3)]
        private bool LINQEntityKeepOriginal
        {
            get
            {
                return this._isKeepOriginal;
            }
            set
            {
                this._isKeepOriginal = value;
            }
        }

        [DataMember(Order = 4)]
        private LINQEntityBase LINQEntityOriginalValue
        {
            get
            {
                return this._originalEntityValue;
            }
            set
            {
                this._originalEntityValue = value;
            }
        }

        [DataMember(Order = 5)]
        private List<LINQEntityBase> LINQEntityDetachedEntities
        {
            get
            {
                bool? isRoot = this.IsRoot;
                if ((!isRoot.GetValueOrDefault() ? 0 : (isRoot.HasValue ? 1 : 0)) == 0)
                    return (List<LINQEntityBase>)null;
                List<LINQEntityBase> list = new List<LINQEntityBase>();
                list.AddRange(Enumerable.Where<LINQEntityBase>((IEnumerable<LINQEntityBase>)this._changeTrackingReferences, (Func<LINQEntityBase, bool>)(e => e.LINQEntityState == EntityState.Detached && e != this)));
                return list;
            }
            set
            {
                this._changeTrackingReferences = value;
            }
        }

        [DataMember(Order = 6)]
        private bool? IsRoot
        {
            get
            {
                return this._isRoot ? new bool?(true) : new bool?();
            }
            set
            {
                if (!value.HasValue)
                    this._isRoot = false;
                else
                    this._isRoot = value.Value;
            }
        }

        [DataMember(Order = 1)]
        public string LINQEntityGUID
        {
            get
            {
                return this._entityGUID;
            }
            private set
            {
                this._entityGUID = value;
            }
        }

        [DataMember(Order = 2)]
        public EntityState LINQEntityState
        {
            get
            {
                return this._entityState;
            }
            private set
            {
                this._entityState = value;
            }
        }

        public string Error
        {
            get
            {
                return this.Validate("");
            }
        }

        public string this[string columnName]
        {
            get
            {
                return this.Validate(columnName);
            }
        }

        [DataMember]
        public BusinessValidationResult BusinessValidationResult { get; set; }

        static LINQEntityBase()
        {
            LINQEntityBase.GetImportantProperties();
        }

        protected LINQEntityBase()
        {
            this._entityGUID = Guid.NewGuid().ToString();
            this.Init();
            this.BindToEntityEvents();
        }

        private static List<Type> GetKnownTypes()
        {
            return Enumerable.ToList<Type>(Enumerable.Where<Type>((IEnumerable<Type>)Assembly.Load(new AssemblyName("PushVodIngestion.DataProvider")).GetTypes(), (Func<Type, bool>)(a => a.IsSubclassOf(typeof(LINQEntityBase)))));
        }

        public static string SerializeEntity<T>(T entitySource, IEnumerable<Type> KnownTypes)
        {
            DataContractSerializer contractSerializer = KnownTypes != null ? new DataContractSerializer(entitySource.GetType(), KnownTypes) : new DataContractSerializer(entitySource.GetType());
            if ((object)entitySource == null)
                return (string)null;
            StringBuilder output = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(output);
            contractSerializer.WriteObject(writer, (object)entitySource);
            writer.Close();
            return ((object)output).ToString();
        }

        public static object DeserializeEntity(string EntitySource, Type EntityType, IEnumerable<Type> KnownTypes)
        {
            if (EntityType == null)
                return (object)null;
            DataContractSerializer contractSerializer = KnownTypes != null ? new DataContractSerializer(EntityType, KnownTypes) : new DataContractSerializer(EntityType);
            XmlTextReader xmlTextReader = new XmlTextReader((TextReader)new StringReader(EntitySource));
            object obj = contractSerializer.ReadObject((XmlReader)xmlTextReader);
            xmlTextReader.Close();
            return obj;
        }

        public static LINQEntityBase ShallowCopy(LINQEntityBase source)
        {
            PropertyInfo[] properties1 = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties2 = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            LINQEntityBase linqEntityBase = Activator.CreateInstance(source.GetType()) as LINQEntityBase;
            foreach (PropertyInfo propertyInfo in properties1)
            {
                PropertyInfo sourcePropInfo = propertyInfo;
                if (Attribute.GetCustomAttribute((MemberInfo)sourcePropInfo, typeof(ColumnAttribute), false) != null)
                    Enumerable.First<PropertyInfo>(Enumerable.Where<PropertyInfo>((IEnumerable<PropertyInfo>)properties2, (Func<PropertyInfo, bool>)(pi => pi.Name == sourcePropInfo.Name))).SetValue((object)linqEntityBase, sourcePropInfo.GetValue((object)source, (object[])null), (object[])null);
            }
            linqEntityBase.LINQEntityState = EntityState.Original;
            linqEntityBase.LINQEntityGUID = source.LINQEntityGUID;
            return linqEntityBase;
        }

        public static bool ShallowCompare(LINQEntityBase entity1, LINQEntityBase entity2)
        {
            if (!object.ReferenceEquals((object)entity1.GetType(), (object)entity2.GetType()))
                return false;
            PropertyInfo[] properties1 = entity1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties2 = entity2.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return Enumerable.Count(Enumerable.Where(Enumerable.SelectMany(Enumerable.GroupJoin(Enumerable.Where<PropertyInfo>((IEnumerable<PropertyInfo>)properties1, (Func<PropertyInfo, bool>)(p1 => Attribute.GetCustomAttribute((MemberInfo)p1, typeof(ColumnAttribute), false) != null)), Enumerable.Where<PropertyInfo>((IEnumerable<PropertyInfo>)properties2, (Func<PropertyInfo, bool>)(p2 => Attribute.GetCustomAttribute((MemberInfo)p2, typeof(ColumnAttribute), false) != null)), (Func<PropertyInfo, string>)(pi1 => pi1.Name), (Func<PropertyInfo, string>)(pi2 => pi2.Name), (pi1, pij) =>
            {
                var local_0 = new
                {
                    pi1 = pi1,
                    pij = pij
                };
                return local_0;
            }), param0 => Enumerable.DefaultIfEmpty<PropertyInfo>(param0.pij), (param0, pi2) =>
            {
                var local_0 = new
                {
                    Match = object.Equals(param0.pi1.GetValue((object)entity1, (object[])null), pi2.GetValue((object)entity2, (object[])null))
                };
                return local_0;
            }), cr => !cr.Match)) == 0;
        }

        private static void GetImportantProperties()
        {
            LINQEntityBase._cacheAssociationProperties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
            LINQEntityBase._cacheAssociationFKProperties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
            LINQEntityBase._cacheDBGeneratedProperties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
            foreach (Type key in LINQEntityBase.GetKnownTypes())
            {
                LINQEntityBase._cacheAssociationProperties.Add(key, new Dictionary<string, PropertyInfo>());
                LINQEntityBase._cacheAssociationFKProperties.Add(key, new Dictionary<string, PropertyInfo>());
                LINQEntityBase._cacheDBGeneratedProperties.Add(key, new Dictionary<string, PropertyInfo>());
                foreach (PropertyInfo propertyInfo in key.GetProperties())
                {
                    AssociationAttribute associationAttribute = (AssociationAttribute)Attribute.GetCustomAttribute((MemberInfo)propertyInfo, typeof(AssociationAttribute), false);
                    if (associationAttribute != null)
                    {
                        if (!associationAttribute.IsForeignKey)
                            LINQEntityBase._cacheAssociationProperties[key].Add(propertyInfo.Name, propertyInfo);
                        else
                            LINQEntityBase._cacheAssociationFKProperties[key].Add(propertyInfo.Name, propertyInfo);
                    }
                    else
                    {
                        ColumnAttribute columnAttribute = Attribute.GetCustomAttribute((MemberInfo)propertyInfo, typeof(ColumnAttribute), false) as ColumnAttribute;
                        if (columnAttribute != null && columnAttribute.IsDbGenerated)
                            LINQEntityBase._cacheDBGeneratedProperties[key].Add(propertyInfo.Name, propertyInfo);
                    }
                }
            }
        }

        private void Init()
        {
            this._isSyncronisingWithDB = false;
            this._isRoot = false;
            this._entityState = EntityState.NotTracked;
            this._isKeepOriginal = false;
            this._entityTree = new LINQEntityBase.EntityTree(this, LINQEntityBase._cacheAssociationProperties[this.GetType()]);
        }

        private void BindToEntityEvents()
        {
            INotifyPropertyChanged notifyPropertyChanged = this as INotifyPropertyChanged;
            INotifyPropertyChanging propertyChanging = this as INotifyPropertyChanging;
            if (notifyPropertyChanged == null || propertyChanging == null)
                return;
            notifyPropertyChanged.PropertyChanged += new PropertyChangedEventHandler(this.PropertyChanged);
            propertyChanging.PropertyChanging += new PropertyChangingEventHandler(this.PropertyChanging);
        }

        private void PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (this._isSyncronisingWithDB)
                return;
            if (this.LINQEntityState == EntityState.Deleted || this.LINQEntityState == EntityState.CancelNew)
                throw new ApplicationException("You cannot modify a deleted entity");
            if (this.LINQEntityState != EntityState.Original || !this.LINQEntityKeepOriginal || this.LINQEntityOriginalValue != null)
                return;
            this._originalEntityValueTemp = LINQEntityBase.ShallowCopy(this);
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this._isSyncronisingWithDB)
                return;
            PropertyInfo propertyInfo = (PropertyInfo)null;
            if (this.LINQEntityState == EntityState.NotTracked && LINQEntityBase._cacheAssociationFKProperties[this.GetType()].ContainsKey(e.PropertyName) && (LINQEntityBase._cacheAssociationFKProperties[this.GetType()].TryGetValue(e.PropertyName, out propertyInfo) && propertyInfo != null))
            {
                LINQEntityBase linqEntityBase1 = (LINQEntityBase)propertyInfo.GetValue((object)this, (object[])null);
                if (linqEntityBase1 != null && linqEntityBase1.LINQEntityState != EntityState.NotTracked)
                {
                    foreach (LINQEntityBase linqEntityBase2 in this.ToEntityTree())
                        linqEntityBase2.LINQEntityState = EntityState.New;
                }
            }
            if (this.LINQEntityState != EntityState.New && this.LINQEntityState != EntityState.NotTracked && !LINQEntityBase._cacheAssociationProperties[this.GetType()].ContainsKey(e.PropertyName))
            {
                if (LINQEntityBase._cacheAssociationFKProperties[this.GetType()].ContainsKey(e.PropertyName))
                {
                    if (LINQEntityBase._cacheAssociationFKProperties[this.GetType()].TryGetValue(e.PropertyName, out propertyInfo))
                    {
                        if (propertyInfo != null && propertyInfo.GetValue((object)this, (object[])null) == null)
                        {
                            this._originalEntityValue = this._originalEntityValueTemp;
                            this.LINQEntityState = EntityState.Detached;
                        }
                        else if (this.LINQEntityState != EntityState.Modified && this.LINQEntityState != EntityState.Detached)
                        {
                            this._originalEntityValue = this._originalEntityValueTemp;
                            this.LINQEntityState = EntityState.Modified;
                        }
                    }
                }
                else
                {
                    if (LINQEntityBase._cacheDBGeneratedProperties[this.GetType()].TryGetValue(e.PropertyName, out propertyInfo))
                        return;
                    if (this.LINQEntityState == EntityState.Modified && this._originalEntityValue != null)
                    {
                        if (LINQEntityBase.ShallowCompare(this, this._originalEntityValue))
                        {
                            this.LINQEntityState = EntityState.Original;
                            this._originalEntityValue = (LINQEntityBase)null;
                        }
                    }
                    else if (this.LINQEntityState != EntityState.Modified && this.LINQEntityState != EntityState.Detached)
                    {
                        this._originalEntityValue = this._originalEntityValueTemp;
                        this.LINQEntityState = EntityState.Modified;
                    }
                }
            }
            this._originalEntityValueTemp = (LINQEntityBase)null;
        }

        [OnDeserializing]
        private void BeforeDeserializing(StreamingContext sc)
        {
            this.Init();
        }

        [OnDeserialized]
        private void AfterDeserialized(StreamingContext sc)
        {
            if (this.LINQEntityState == EntityState.NotTracked)
            {
                this.BindToEntityEvents();
            }
            else
            {
                bool? isRoot = this.IsRoot;
                if ((!isRoot.GetValueOrDefault() ? 0 : (isRoot.HasValue ? 1 : 0)) == 0)
                    return;
                this._changeTrackingReferences = this.ToEntityTree();
                foreach (LINQEntityBase linqEntityBase in this._changeTrackingReferences)
                    linqEntityBase.BindToEntityEvents();
            }
        }

        private bool GetAssociationProperty(string propName, out PropertyInfo propInfo)
        {
            return LINQEntityBase._cacheAssociationProperties[this.GetType()].TryGetValue(propName, out propInfo);
        }

        private bool GetAssociationFKProperty(string propName, out PropertyInfo propInfo)
        {
            return LINQEntityBase._cacheAssociationFKProperties[this.GetType()].TryGetValue(propName, out propInfo);
        }

        private bool GetDbGeneratedProperty(string propName, out PropertyInfo propInfo)
        {
            return LINQEntityBase._cacheDBGeneratedProperties[this.GetType()].TryGetValue(propName, out propInfo);
        }

        public List<LINQEntityBase> ToEntityTree()
        {
            List<LINQEntityBase> list = Enumerable.ToList<LINQEntityBase>(Enumerable.Select<LINQEntityBase, LINQEntityBase>((IEnumerable<LINQEntityBase>)this._entityTree, (Func<LINQEntityBase, LINQEntityBase>)(t => t)));
            bool? isRoot = this.IsRoot;
            if ((!isRoot.GetValueOrDefault() ? 0 : (isRoot.HasValue ? 1 : 0)) != 0)
                list.AddRange((IEnumerable<LINQEntityBase>)this.LINQEntityDetachedEntities);
            return list;
        }

        public void SetAsChangeTrackingRoot()
        {
            this.SetAsChangeTrackingRoot(EntityState.Original, false);
        }

        public void SetAsChangeTrackingRoot(bool onModifyKeepOriginal)
        {
            this.SetAsChangeTrackingRoot(EntityState.Original, onModifyKeepOriginal);
        }

        public void SetAsChangeTrackingRoot(EntityState initialEntityState)
        {
            this.SetAsChangeTrackingRoot(initialEntityState, false);
        }

        public void SetAsChangeTrackingRoot(EntityState initialEntityState, bool onModifyKeepOriginal)
        {
            int num;
            if (this.LINQEntityState != EntityState.NotTracked)
            {
                bool? isRoot = this.IsRoot;
                num = (isRoot.GetValueOrDefault() ? 0 : (isRoot.HasValue ? 1 : 0)) == 0 ? 1 : 0;
            }
            else
                num = 1;
            if (num == 0)
                throw new ApplicationException("This entity is already being Change Tracked and cannot be the root.");
            if (initialEntityState == EntityState.Modified)
                throw new ApplicationException("An Entity cannot be set as the Change Tracking Root whilst modified.  Instead, Set as Change Tracking root and then modify the entity.");
            if (initialEntityState == EntityState.Detached)
                throw new ApplicationException("An Entity cannot be set as the Change Tracking Root whilst detached.");
            this._changeTrackingReferences = Enumerable.ToList<LINQEntityBase>((IEnumerable<LINQEntityBase>)this._entityTree);
            this._isRoot = true;
            foreach (LINQEntityBase linqEntityBase in this._changeTrackingReferences)
            {
                linqEntityBase.LINQEntityState = initialEntityState != EntityState.Deleted ? initialEntityState : (this != linqEntityBase ? EntityState.Original : EntityState.Deleted);
                linqEntityBase.LINQEntityOriginalValue = (LINQEntityBase)null;
                linqEntityBase.LINQEntityKeepOriginal = onModifyKeepOriginal;
            }
        }

        public void SynchroniseWithDataContext(DataContext targetDataContext)
        {
            this.SynchroniseWithDataContext(targetDataContext, true);
        }

        public void SynchroniseWithDataContext(DataContext targetDataContext, bool cascadeDelete)
        {
            if (targetDataContext.DeferredLoadingEnabled)
                throw new ApplicationException("Syncronisation requires that the Deferred loading is disabled on the Target DataContext");
            bool? isRoot = this.IsRoot;
            if ((isRoot.GetValueOrDefault() ? 0 : (isRoot.HasValue ? 1 : 0)) != 0)
                throw new ApplicationException("You cannot syncronise an entity that is not the change tracking root");
            List<LINQEntityBase> list1 = Enumerable.ToList<LINQEntityBase>(Enumerable.Distinct<LINQEntityBase>((IEnumerable<LINQEntityBase>)this.ToEntityTree()));
            List<LINQEntityBase> list2 = new List<LINQEntityBase>();
            try
            {
                foreach (LINQEntityBase linqEntityBase in list1)
                    linqEntityBase._isSyncronisingWithDB = true;
                foreach (LINQEntityBase linqEntityBase in list1)
                {
                    if (linqEntityBase.LINQEntityState == EntityState.CancelNew)
                    {
                        foreach (PropertyInfo propertyInfo in LINQEntityBase._cacheAssociationFKProperties[linqEntityBase.GetType()].Values)
                            propertyInfo.SetValue((object)linqEntityBase, (object)null, (object[])null);
                    }
                }
                foreach (LINQEntityBase linqEntityBase1 in list1)
                {
                    if (linqEntityBase1.LINQEntityState != EntityState.Original)
                    {
                        if (linqEntityBase1.LINQEntityState == EntityState.New)
                        {
                            foreach (PropertyInfo propertyInfo in LINQEntityBase._cacheAssociationFKProperties[linqEntityBase1.GetType()].Values)
                            {
                                LINQEntityBase linqEntityBase2 = propertyInfo.GetValue((object)linqEntityBase1, (object[])null) as LINQEntityBase;
                                if (linqEntityBase2 != null && linqEntityBase2.LINQEntityState != EntityState.New)
                                {
                                    try
                                    {
                                        targetDataContext.GetTable(linqEntityBase2.GetType()).Attach((object)linqEntityBase2, false);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            targetDataContext.GetTable(linqEntityBase1.GetEntityType()).InsertOnSubmit((object)linqEntityBase1);
                        }
                        else if (linqEntityBase1.LINQEntityState == EntityState.Modified || linqEntityBase1.LINQEntityState == EntityState.Detached)
                        {
                            if (linqEntityBase1.LINQEntityOriginalValue != null)
                                targetDataContext.GetTable(linqEntityBase1.GetEntityType()).Attach((object)linqEntityBase1, (object)linqEntityBase1.LINQEntityOriginalValue);
                            else
                                targetDataContext.GetTable(linqEntityBase1.GetEntityType()).Attach((object)linqEntityBase1, true);
                        }
                    }
                    if (linqEntityBase1.LINQEntityState == EntityState.Deleted && !list2.Contains(linqEntityBase1))
                    {
                        if (cascadeDelete)
                        {
                            List<LINQEntityBase> list3 = linqEntityBase1.ToEntityTree();
                            list3.Reverse();
                            foreach (LINQEntityBase linqEntityBase2 in list3)
                            {
                                if (!list2.Contains(linqEntityBase2))
                                {
                                    linqEntityBase2.SetAsDeleteOnSubmit();
                                    targetDataContext.GetTable(linqEntityBase2.GetEntityType()).Attach((object)linqEntityBase2);
                                    targetDataContext.GetTable(linqEntityBase2.GetEntityType()).DeleteOnSubmit((object)linqEntityBase2);
                                    list2.Add(linqEntityBase2);
                                }
                            }
                        }
                        else
                        {
                            targetDataContext.GetTable(linqEntityBase1.GetEntityType()).Attach((object)linqEntityBase1);
                            targetDataContext.GetTable(linqEntityBase1.GetEntityType()).DeleteOnSubmit((object)linqEntityBase1);
                            list2.Add(linqEntityBase1);
                        }
                        if (this == linqEntityBase1)
                            break;
                    }
                }
                this.SetAsChangeTrackingRoot(this.LINQEntityKeepOriginal);
            }
            finally
            {
                foreach (LINQEntityBase linqEntityBase in list1)
                    linqEntityBase._isSyncronisingWithDB = false;
            }
        }

        public void SetAsInsertOnSubmit(bool ApplyToChildEntities)
        {
            if (ApplyToChildEntities)
            {
                foreach (LINQEntityBase linqEntityBase in Enumerable.ToList<LINQEntityBase>(Enumerable.Distinct<LINQEntityBase>((IEnumerable<LINQEntityBase>)this.ToEntityTree())))
                    linqEntityBase.SetAsInsertOnSubmit();
            }
            else
                this.SetAsInsertOnSubmit();
        }

        public void SetAsInsertOnSubmit()
        {
            if (this.LINQEntityState == EntityState.Detached)
                throw new ApplicationException("You cannot change the Entity State from 'Detached' to 'New'");
            if (this.LINQEntityState == EntityState.NotTracked)
                throw new ApplicationException("You cannot change the Entity State when the Entity is not change tracked");
            this.LINQEntityState = EntityState.New;
        }

        public void SetAsUpdateOnSubmit()
        {
            if (this._originalEntityValue != null)
                this.SetAsUpdateOnSubmit(this._originalEntityValue);
            else
                this.SetAsUpdateOnSubmit((LINQEntityBase)null);
        }

        public void SetAsUpdateOnSubmit(bool ApplyToChildEntities)
        {
            if (ApplyToChildEntities)
            {
                foreach (LINQEntityBase linqEntityBase in Enumerable.ToList<LINQEntityBase>(Enumerable.Distinct<LINQEntityBase>((IEnumerable<LINQEntityBase>)this.ToEntityTree())))
                    linqEntityBase.SetAsUpdateOnSubmit();
            }
            else
                this.SetAsUpdateOnSubmit();
        }

        public void SetAsUpdateOnSubmit(LINQEntityBase OriginalValue)
        {
            if (this.LINQEntityState == EntityState.Detached)
                throw new ApplicationException("You cannot change the Entity State from 'Detached' to 'Modified'");
            if (this.LINQEntityState == EntityState.NotTracked)
                throw new ApplicationException("You cannot change the Entity State when the Entity is not change tracked");
            this._originalEntityValue = OriginalValue == null ? (LINQEntityBase)null : LINQEntityBase.ShallowCopy(this);
            this.LINQEntityState = EntityState.Modified;
        }

        public void SetAsNoChangeOnSubmit(bool ApplyToChildEntities)
        {
            if (ApplyToChildEntities)
            {
                foreach (LINQEntityBase linqEntityBase in Enumerable.ToList<LINQEntityBase>(Enumerable.Distinct<LINQEntityBase>((IEnumerable<LINQEntityBase>)this.ToEntityTree())))
                    linqEntityBase.SetAsNoChangeOnSubmit();
            }
            else
                this.SetAsNoChangeOnSubmit();
        }

        public void SetAsNoChangeOnSubmit()
        {
            if (this.LINQEntityState == EntityState.Detached)
                throw new ApplicationException("You cannot change the Entity State from 'Detached' to 'Original'");
            if (this.LINQEntityState == EntityState.NotTracked)
                throw new ApplicationException("You cannot change the Entity State when the Entity is not change tracked");
            this.LINQEntityState = EntityState.Original;
        }

        public void SetAsDeleteOnSubmit(bool ApplyToChildEntities)
        {
            if (ApplyToChildEntities)
            {
                foreach (LINQEntityBase linqEntityBase in Enumerable.ToList<LINQEntityBase>(Enumerable.Distinct<LINQEntityBase>((IEnumerable<LINQEntityBase>)this.ToEntityTree())))
                    linqEntityBase.SetAsDeleteOnSubmit();
            }
            else
                this.SetAsDeleteOnSubmit();
        }

        public void SetAsDeleteOnSubmit()
        {
            if (this.LINQEntityState == EntityState.Detached)
                throw new ApplicationException("You cannot modify the Entity State from 'Detached' to 'Delete' ");
            if (this.LINQEntityState == EntityState.NotTracked)
                throw new ApplicationException("You cannot change the Entity State when the Entity is not change tracked");
            if (this.LINQEntityState == EntityState.New)
            {
                this.LINQEntityState = EntityState.CancelNew;
            }
            else
            {
                if (this.LINQEntityState == EntityState.CancelNew)
                    return;
                this.LINQEntityState = EntityState.Deleted;
            }
        }

        public Type GetEntityType()
        {
            Type type = this.GetType();
            for (TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute((MemberInfo)type, typeof(TableAttribute), false); tableAttribute == null && type != typeof(LINQEntityBase); tableAttribute = (TableAttribute)Attribute.GetCustomAttribute((MemberInfo)type, typeof(TableAttribute), false))
                type = type.BaseType;
            return type;
        }

        protected virtual string Validate(string columnName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            switch (columnName)
            {
                case "Phone":
                    stringBuilder.Append("Phone is either 0 or is not a number");
                    break;
            }
            return string.Format("{0}", (object)((object)stringBuilder).ToString());
        }

        public virtual BusinessValidationResult ValidateBusinessRule()
        {
            return new BusinessValidationResult();
        }

        private class EntityTree : IEnumerable<LINQEntityBase>, IEnumerable
        {
            private Dictionary<string, PropertyInfo> _entityAssociationProperties;
            private LINQEntityBase _entityRoot;

            public EntityTree(LINQEntityBase EntityRoot, Dictionary<string, PropertyInfo> EntityAssociationProperties)
            {
                this._entityRoot = EntityRoot;
                this._entityAssociationProperties = EntityAssociationProperties;
            }

            public IEnumerator<LINQEntityBase> GetEnumerator()
            {
                yield return this._entityRoot;
                foreach (PropertyInfo propertyInfo in this._entityAssociationProperties.Values)
                {
                    if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(EntitySet<>))
                    {
                        IEnumerator entityList = (propertyInfo.GetValue((object)this._entityRoot, (object[])null) as IEnumerable).GetEnumerator();
                        while (entityList.MoveNext())
                        {
                            if (entityList.Current.GetType().IsSubclassOf(typeof(LINQEntityBase)))
                            {
                                LINQEntityBase currentEntity = (LINQEntityBase)entityList.Current;
                                foreach (LINQEntityBase linqEntityBase in currentEntity.ToEntityTree())
                                    yield return linqEntityBase;
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsSubclassOf(typeof(LINQEntityBase)) && propertyInfo.GetValue((object)this._entityRoot, (object[])null) != null)
                    {
                        foreach (LINQEntityBase linqEntityBase in (propertyInfo.GetValue((object)this._entityRoot, (object[])null) as LINQEntityBase).ToEntityTree())
                            yield return linqEntityBase;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)this.GetEnumerator();
            }
        }
    }
}
