﻿using CoreProfiler;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PracticeWebAPIDemo.WebApi.Infrastructure.ActionFilters
{
    /// <summary>
    /// class CoreProfilingAttribute.
    /// </summary>
    /// <seealso cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class CoreProfilingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// ProfilingName.
        /// </summary>
        /// <value>The name of the profiling.</value>
        public string ProfilingName { get; set; }

        /// <summary>
        /// ProfilingStep.
        /// </summary>
        /// <value>The profiling step.</value>
        public IDisposable ProfilingStep { get; set; }

        /// <summary>
        /// Called when [action executing].
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (string.IsNullOrEmpty(this.ProfilingName))
            {
                this.ProfilingName = context.ActionDescriptor.DisplayName;
            }

            this.ProfilingStep = ProfilingSession.Current.Step(this.ProfilingName);
        }

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            this.ProfilingStep?.Dispose();
        }
    }
}
