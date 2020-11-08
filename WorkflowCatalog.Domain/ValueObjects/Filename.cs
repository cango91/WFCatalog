using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Text;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.ValueObjects
{
    public class Filename : ValueObject
    {
        private Filename()
        {
        }

        public Filename(string filename)
        {
            try
            {
                var index = filename.LastIndexOf('.');
                Name = filename.Substring(0, index);
                Extension = filename.Substring(index + 1);
            }
            catch (Exception ex)
            {
                throw new FileNameException(nameof(Filename), ex);
            }
        }

        public Filename(string name, string ext)
        {
            Name = name;
            Extension = ext; 
        }

        public static Filename For(string filename)
        {
            var file = new Filename();
            try
            {
                var index = filename.LastIndexOf('.');
                file.Name = filename.Substring(0, index);
                file.Extension = filename.Substring(index + 1);

            }
            catch (Exception ex)
            {
                throw new FileNameException(filename, ex);
            }
            return file;
        }

        public string Name { get; private set; }
        public string Extension { get; private set; }

        public static implicit operator string(Filename filename)
        {
            return filename.ToString();
        }
        
        public static explicit operator Filename(string filename)
        {
            return For(filename);
        }

        public override string ToString()
        {
            return $"{Name}.{Extension}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Extension;
        }


    }
}
