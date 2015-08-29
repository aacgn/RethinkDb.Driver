﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using RethinkDb.Driver.Model;
using RethinkDb.Driver.Net;
using RethinkDb.Driver.Proto;

namespace RethinkDb.Driver.Ast
{

	/// <summary>
	/// Base class for all reql queries.
	/// </summary>
	public class ReqlAst
	{
	    protected internal ReqlAst Prev { get; }
	    protected internal TermType TermType { get; }
	    protected internal Arguments Args { get; }
	    protected internal OptArgs OptArgs { get; }

	    protected internal ReqlAst(ReqlAst prev, TermType termType, Arguments args, OptArgs optargs)
        {
            this.Prev = prev;
            this.TermType = termType;
            this.Args = new Arguments();
            if( prev != null ) // TopLevel should have prev = null
            { 
                this.Args.Add(prev);
            }
            if( args != null )
            {
                this.Args.AddRange(args);
            }
            this.OptArgs = optargs != null ? optargs : new OptArgs();
        }

        protected internal ReqlAst(TermType termType, Arguments args) : this(null, termType, args, null)
        {
        }

        protected internal virtual object Build()
        {
            // Create a JSON object from the Ast
            JArray list = new JArray();
            list.Add(TermType);
            if( Args.Count > 0 )
            {
                var collect = Args.Select(a =>
                    {
                        return a.Build();
                    });
                list.Add(new JArray(collect));
            }
            else
            {
                list.Add(new JArray());
            }

            if( OptArgs.Count > 0 )
            {
                JObject joptargs = new JObject();
                foreach( KeyValuePair<string, ReqlAst> entry in OptArgs )
                {
                    joptargs.Add(entry.Key, (JToken)entry.Value.Build());
                }
                list.Add(joptargs);
            }
            return list;
        }

        public virtual T run<T>(Connection conn, GlobalOptions g)
        {
            return (T)conn.run<T>(this, g);
        }

        public virtual T run<T>(Connection conn)
        {
            return (T)conn.run<T>(this, new GlobalOptions());
        }
        
    }

}