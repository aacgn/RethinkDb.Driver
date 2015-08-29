








//AUTOGENERATED, DO NOTMODIFY.
//Do not edit this file directly.

// ReSharper disable CheckNamespace

using RethinkDb.Driver.Ast;
using RethinkDb.Driver.Model;
using RethinkDb.Driver.Proto;
using System.Collections.Generic;

namespace RethinkDb.Driver.Ast {
    public class Eq : ReqlQuery {
    
    
        public Eq (object arg) : this(new Arguments(arg), null) {
        }
        public Eq (Arguments args, OptArgs optargs) : this(null, args, optargs) {
        }
        public Eq (ReqlAst prev, Arguments args, OptArgs optargs)
             : this(prev, TermType.EQ, args, optargs) {
        }

    protected Eq (ReqlAst previous, TermType termType, Arguments args, OptArgs optargs) : base(previous, termType, args, optargs)
    {
    }


    

    /* Static Factories */

        public static Eq FromArgs(params object[] args){
         return new Eq (new Arguments(args), null);
        }


    

    


    
    }
}