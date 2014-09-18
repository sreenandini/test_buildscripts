/* ================================================================================= 
 * Purpose		:	WFP Performance Improvement Methods
 * File Name	:   WPFPerfMethods.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	07/08/2011
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 07/08/2011		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using BMC.Common.ExceptionManagement;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using BMC.Common.LogManagement;
using System.Runtime.Remoting.Channels.Tcp;
using BMCIPC;
using Microsoft.Win32;
using System.Runtime.Remoting.Channels;
using BMC.Common.Utilities;

namespace BMC.Presentation.POS.Helper_classes
{
    #region WPFPerfMethods
    /// <summary>
    /// WFP Performance Improvement Methods
    /// </summary>
    public static class WPFPerfMethods
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WPFPerfMethods"/> class.
        /// </summary>
        static WPFPerfMethods() { }
        #endregion

        #region Static Methods

        /// <summary>
        /// Clears all bindings.
        /// </summary>
        /// <param name="objects">The objects.</param>
        public static void ClearAllBindings(params DependencyObject[] objects)
        {
            foreach (DependencyObject obj in objects)
            {
                if (obj != null)
                {
                    BindingOperations.ClearAllBindings(obj);
                }
            }
        }

        /// <summary>
        /// Clears all bindings.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void ClearAllBindings(this DependencyObject source)
        {
            if (source != null)
            {
                BindingOperations.ClearAllBindings(source);
            }
        }

        /// <summary>
        /// Clears all self child bindings.
        /// </summary>
        /// <param name="objects">The objects.</param>
        public static void ClearAllSelfChildBindings(params DependencyObject[] objects)
        {
            ClearAllSelfChildBindings(null, objects);

        }

        /// <summary>
        /// Clears all self child bindings.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="objects">The objects.</param>
        public static void ClearAllSelfChildBindings(Action<DependencyObject> action, params DependencyObject[] objects)
        {
            foreach (DependencyObject obj in objects)
            {
                if (obj != null)
                {
                    BindingOperations.ClearAllBindings(obj);
                    if (action != null) action(obj);
                    obj.IterateVisualChild((c) =>
                    {
                        BindingOperations.ClearAllBindings(c);
                        if (action != null) action(c);
                    });
                }
            }
        }

        /// <summary>
        /// Clears the dependency properties.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="properties">The properties.</param>
        public static void ClearDependencyProperties(this DependencyObject source, params DependencyProperty[] properties)
        {
            if (properties != null)
            {
                foreach (DependencyProperty dp in properties)
                {
                    source.ClearValue(dp);
                }
            }
        }

        /// <summary>
        /// Clears all dependency properties.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void ClearAllDependencyProperties(this DependencyObject source)
        {
            try
            {
                PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(source,
                    new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) });
                if (descriptors != null)
                {
                    foreach (PropertyDescriptor descriptor in descriptors)
                    {
                        DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(descriptor);
                        if (dpd != null)
                        {
                            DependencyProperty dp = dpd.DependencyProperty;
                            if (dp != null)
                            {
                                try
                                {
                                    // Clear the value
                                    if (!dp.ReadOnly)
                                    {
                                        source.ClearValue(dp);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Removes the property changed callback.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        public static void RemovePropertyChangedCallback(this PropertyMetadata metadata)
        {
            try
            {
                if (metadata != null)
                {
                    if (metadata.PropertyChangedCallback != null)
                    {
                        Delegate[] handlers = metadata.PropertyChangedCallback.GetInvocationList();
                        if (handlers != null)
                        {
                            PropertyChangedCallback headHandler = (PropertyChangedCallback)handlers[0];
                            for (int i = 0; i < handlers.Length; i++)
                            {
                                headHandler -= (PropertyChangedCallback)handlers[i];
                            }
                            metadata.PropertyChangedCallback = headHandler;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Stops the and remove story board.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="storyboardResource">The storyboard resource.</param>
        public static void StopAndRemoveStoryBoard(this FrameworkElement owner, string storyboardResource)
        {
            StopAndRemoveStoryBoard(owner, owner.FindResource(storyboardResource) as Storyboard);
        }

        /// <summary>
        /// Stops the and remove story board.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="storyboardResources">The storyboard resources.</param>
        public static void StopAndRemoveStoryBoard(this FrameworkElement owner, params string[] storyboardResources)
        {
            foreach (string storyboardResource in storyboardResources)
            {
                StopAndRemoveStoryBoard(owner, storyboardResource);
            }
        }

        /// <summary>
        /// Stops the and remove story board.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="storyboards">The storyboards.</param>
        public static void StopAndRemoveStoryBoard(this FrameworkElement owner, params Storyboard[] storyboards)
        {
            foreach (Storyboard storyboard in storyboards)
            {
                StopAndRemoveStoryBoard(owner, storyboard);
            }
        }

        /// <summary>
        /// Stops the and remove story board.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="storyboard">The storyboard.</param>
        public static void StopAndRemoveStoryBoard(this FrameworkElement owner, Storyboard storyboard)
        {
            try
            {
                if (storyboard != null)
                {
                    storyboard.ClearAllBindings();
                    if (owner != null)
                    {
                        storyboard.Stop(owner);
                        storyboard.Remove(owner);
                    }
                    else
                    {
                        storyboard.Stop();
                        storyboard.Remove();
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Warning);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Iterates the visual child.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="action">The action.</param>
        public static void IterateVisualChild(this DependencyObject parent, Action<DependencyObject> action)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (action != null) action(child);
                IterateVisualChild(child, action);
            }
        }

        /// <summary>
        /// Finds the name of the visual child by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static T FindVisualChildByName<T>(this DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Removes the child adorner layer layout updated.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void RemoveChildAdornerLayerLayoutUpdated(this Visual source)
        {
            source.IterateVisualChild((c) =>
            {
                if (c is AdornerLayer)
                    RemoveAdornerLayerLayoutUpdated((AdornerLayer)c);
            });
        }

        /// <summary>
        /// Removes the adorner layer layout updated.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void RemoveAdornerLayerLayoutUpdated(this AdornerLayer source)
        {
            if (source != null)
            {
                RemoveEventHandler(source, "LayoutUpdated", "OnLayoutUpdated");
            }
        }

        /// <summary>
        /// Removes the event handler.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="callBackName">Name of the call back.</param>
        public static void RemoveEventHandler(this object publisher, string eventName, string callBackName)
        {
            RemoveEventHandler(publisher, eventName, publisher, callBackName);
        }

        /// <summary>
        /// Removes the event handler.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="callBackName">Name of the call back.</param>
        public static void RemoveEventHandler(this object publisher, string eventName, object subscriber, string callBackName)
        {
            RemoveEventHandler(publisher, publisher.GetType(), BindingFlags.Public | BindingFlags.Instance, eventName, subscriber, callBackName);
        }

        /// <summary>
        /// Removes the event handler.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="callBackName">Name of the call back.</param>
        public static void RemoveEventHandler(this object publisher, Type publisherType, BindingFlags bindingFlags, string eventName, object subscriber, string callBackName)
        {
            EventInfoDelegate eid = GetEventInfoDelegate(publisher, publisherType, eventName, bindingFlags, subscriber, callBackName);
            if (eid != null)
            {
                try
                {
                    eid.Info.RemoveEventHandler(publisher, eid.Callback);
                }
                catch (System.InvalidOperationException)
                {
                    MethodInfo mi = eid.Info.GetRemoveMethod(true);
                    if (mi != null)
                    {
                        mi.Invoke(publisher, new object[] { eid.Callback });
                    }
                }
            }
        }

        /// <summary>
        /// Gets the event info delegate.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="callbackName">Name of the callback.</param>
        /// <returns>Event Info Delegate.</returns>
        public static EventInfoDelegate GetEventInfoDelegate(this object publisher, string eventName, string callbackName)
        {
            return GetEventInfoDelegate(publisher, eventName, BindingFlags.Public | BindingFlags.Instance, publisher, callbackName);
        }

        /// <summary>
        /// Gets the event info delegate.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="callbackName">Name of the callback.</param>
        /// <returns>Event Info Delegate.</returns>
        public static EventInfoDelegate GetEventInfoDelegate(this object publisher, string eventName, BindingFlags bindingFlags, string callbackName)
        {
            return GetEventInfoDelegate(publisher, eventName, bindingFlags, publisher, callbackName);
        }

        /// <summary>
        /// Gets the event info delegate.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="callbackName">Name of the callback.</param>
        /// <returns>Event Info Delegate.</returns>
        public static EventInfoDelegate GetEventInfoDelegate(this object publisher, string eventName, object subscriber, string callbackName)
        {
            return GetEventInfoDelegate(publisher, eventName, BindingFlags.Public | BindingFlags.Instance, subscriber, callbackName);
        }

        /// <summary>
        /// Gets the event info delegate.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="callbackName">Name of the callback.</param>
        /// <returns>Event Info Delegate.</returns>
        public static EventInfoDelegate GetEventInfoDelegate(this object publisher, string eventName, BindingFlags bindingFlags, object subscriber, string callbackName)
        {
            return GetEventInfoDelegate(publisher, publisher.GetType(), eventName, bindingFlags, subscriber, callbackName);
        }

        /// <summary>
        /// Gets the event info delegate.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="publisherType">Type of the publisher.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="callbackName">Name of the callback.</param>
        /// <returns>Event Info Delegate.</returns>
        public static EventInfoDelegate GetEventInfoDelegate(this object publisher, Type publisherType, string eventName, BindingFlags bindingFlags, object subscriber, string callbackName)
        {
            if (subscriber == null) return null;
            EventInfo ei = publisherType.GetEvent(eventName, bindingFlags);
            if (ei == null) return null;
            Delegate del = Delegate.CreateDelegate(ei.EventHandlerType, subscriber, callbackName);
            return new EventInfoDelegate(ei, del);
        }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <typeparam name="S">Type of the source.</typeparam>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns>Field value</returns>
        public static T GetFieldValue<S, T>(this S source, string fieldName, BindingFlags bindingFlags)
        {
            FieldInfo fldDP = typeof(S).GetField(fieldName, bindingFlags);
            if (fldDP != null)
            {
                return (T)fldDP.GetValue(source);
            }
            return default(T);
        }

        /// <summary>
        /// Gets the property info.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <param name="types">The types.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Property Info.</returns>
        public static PropertyInfo GetPropertyInfo(this object source, Type sourceType, string propertyName, BindingFlags bindingFlags, Type returnType, Type[] types, object[] parameters)
        {
            if (types == null) types = new Type[0];
            return sourceType.GetProperty(propertyName, bindingFlags, null, returnType, types, null);
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <returns>Property value.</returns>
        public static object GetPropertyValue(this object source, string propertyName, BindingFlags bindingFlags, Type returnType)
        {
            return GetPropertyValue(source, propertyName, bindingFlags, returnType, null, null);
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <param name="types">The types.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Property value.</returns>
        public static object GetPropertyValue(this object source, string propertyName, BindingFlags bindingFlags, Type returnType, Type[] types, object[] parameters)
        {
            return GetPropertyValue(source, source.GetType(), propertyName, bindingFlags, returnType, types, parameters);
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <param name="types">The types.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Property value.</returns>
        public static object GetPropertyValue(this object source, Type sourceType, string propertyName, BindingFlags bindingFlags, Type returnType, Type[] types, object[] parameters)
        {
            PropertyInfo pi = GetPropertyInfo(source, sourceType, propertyName, bindingFlags, returnType, types, parameters);
            if (pi != null)
            {
                return pi.GetValue(source, null);
            }
            return null;
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="source">The source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="types">The types.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Property value.</returns>
        public static T GetPropertyValue<S, T>(this S source, string propertyName, BindingFlags bindingFlags, Type[] types, object[] parameters)
        {
            PropertyInfo pi = GetPropertyInfo(source, typeof(S), propertyName, bindingFlags, typeof(T), types, parameters);
            if (pi != null)
            {
                return (T)pi.GetValue(source, null);
            }
            return default(T);
        }

        /// <summary>
        /// Invokes the method from property.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingFlags2">The binding flags2.</param>
        /// <returns>Method result.</returns>
        public static object InvokeMethodFromProperty(this object source, string propertyName, BindingFlags bindingFlags, Type propertyType, string methodName, BindingFlags bindingFlags2)
        {
            object piv = GetPropertyValue(source, propertyName, bindingFlags, propertyType);
            if (piv != null)
            {
                return InvokeMethod(piv as DependencyObject, methodName, bindingFlags2);
            }
            return null;
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns>Method result.</returns>
        public static object InvokeMethod(this object source, string methodName, BindingFlags bindingFlags)
        {
            return InvokeMethod(source, methodName, bindingFlags, null, null);
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="types">The types.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Method result.</returns>
        public static object InvokeMethod(this object source, string methodName, BindingFlags bindingFlags, Type[] types, object[] parameters)
        {
            return InvokeMethod(source, source.GetType(), methodName, bindingFlags, types, parameters);
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="types">The types.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Method result.</returns>
        public static object InvokeMethod(this object source, Type sourceType, string methodName, BindingFlags bindingFlags, Type[] types, object[] parameters)
        {
            if (types == null) types = new Type[0];
            MethodInfo mi = sourceType.GetMethod(methodName, bindingFlags, null, types, null);
            if (mi != null)
            {
                return mi.Invoke(source, parameters);
            }
            return null;
        }

        /// <summary>
        /// Gets the dependency property.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns>Dependency Property Instance.</returns>
        public static DependencyProperty GetDependencyProperty(this DependencyObject source, Type sourceType,
            string propertyName, BindingFlags bindingFlags)
        {
            FieldInfo fldDP = sourceType.GetField(propertyName, bindingFlags);
            if (fldDP != null)
            {
                return fldDP.GetValue(source)
                    as DependencyProperty;
            }
            return null;
        }

        /// <summary>
        /// Invokes the on throttle background timeout.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void InvokeOnThrottleBackgroundTimeout(this PasswordBox source)
        {
            FrameworkElement renderScope = GetPropertyValue(source, "RenderScope",
                BindingFlags.Instance | BindingFlags.NonPublic, typeof(FrameworkElement)) as FrameworkElement;
            if (renderScope != null)
            {
                FieldInfo fiTimer = renderScope.GetType().GetField("_throttleBackgroundTimer", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fiTimer != null)
                {
                    DispatcherTimer timer = fiTimer.GetValue(renderScope) as DispatcherTimer;
                    if (timer != null)
                    {
                        // Remove Tick event handler first
                        RemoveEventHandler(timer, "Tick", renderScope, "OnThrottleBackgroundTimeout");

                        // Fire the callback method
                        InvokeMethod(renderScope, "OnThrottleBackgroundTimeout", BindingFlags.Instance | BindingFlags.NonPublic,
                            new Type[] { typeof(object), typeof(EventArgs) }, new object[] { renderScope, EventArgs.Empty });
                    }
                }
            }
        }

        /// <summary>
        /// Removes the bindings render close.
        /// </summary>
        /// <param name="typeOfChild">The type of child.</param>
        /// <param name="objects">The objects.</param>
        public static void RemoveBindingsRenderClose(Type typeOfChild, params DependencyObject[] objects)
        {
            WPFPerfMethods.ClearAllSelfChildBindings((c) =>
            {
                if (c.GetType() == typeOfChild)
                {
                    ((UIElement)c).RenderClose();
                }
            }, objects);
        }

        /// <summary>
        /// Renders the close.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void RenderClose(this UIElement element)
        {
            try
            {
                InvokeMethod(element, typeof(UIElement),
                            "RenderClose",
                            BindingFlags.Instance | BindingFlags.NonPublic,
                            new Type[] { typeof(System.Windows.Media.Drawing).GetInterface("IDrawingContent") },
                            new object[] { null });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Clears the self and child visual transform.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void ClearSelfAndChildVisualTransform(this Visual element)
        {
            element.IterateVisualChild((c) =>
            {
                if (c is Visual)
                {
                    ((Visual)c).ClearVisualTransform();
                }
            });
        }

        /// <summary>
        /// Clears the visual transform.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void ClearVisualTransform(this Visual element)
        {
            try
            {
                PropertyInfo pi = element.GetPropertyInfo(typeof(Visual), "VisualTransform",
                     BindingFlags.Instance | BindingFlags.NonPublic, typeof(Transform),
                     null, null);
                if (pi != null)
                {
                    pi.SetValue(element, null, null);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Detaches from visual tree.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        public static void DetachFromVisualTree(this PasswordBox publisher)
        {
            publisher.InvokeMethod(typeof(PasswordBox), "DetachFromVisualTree", BindingFlags.Instance | BindingFlags.NonPublic, null, null);
        }

        /// <summary>
        /// Detaches from visual tree.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        public static void DetachFromVisualTree(this TextBoxBase publisher)
        {
            publisher.InvokeMethod(typeof(TextBoxBase), "DetachFromVisualTree", BindingFlags.Instance | BindingFlags.NonPublic, null, null);
        }

        /// <summary>
        /// Clears the triggers.
        /// </summary>
        /// <param name="triggers">The triggers.</param>
        public static void ClearTriggers(this FrameworkElement source)
        {
            TriggerCollection triggers = source.Triggers;
            if (triggers != null)
            {
                foreach (TriggerBase trigger in triggers)
                {
                    EventTrigger trgEvent = trigger as EventTrigger;
                    if (trigger != null)
                    {
                        // Find the owner from EventTrigger itself
                        FrameworkElement owner = trgEvent.GetFieldValue<EventTrigger, FrameworkElement>("_source", 
                            BindingFlags.Instance | BindingFlags.NonPublic);

                        // clean the trigger
                        trgEvent.CleanupEventTrigger(source);
                        
                        // remove the trigger actions
                        RemoveTriggerActions(owner, trgEvent.Actions);
                        RemoveTriggerActions(owner, trgEvent.EnterActions);
                        RemoveTriggerActions(owner, trgEvent.ExitActions);
                    }
                }
            }
        }

        /// <summary>
        /// Removes the trigger actions.
        /// </summary>
        /// <param name="actions">The actions.</param>
        private static void RemoveTriggerActions(FrameworkElement owner, TriggerActionCollection actions)
        {
            if (actions != null)
            {
                foreach (TriggerAction action in actions)
                {
                    if (action is BeginStoryboard)
                    {
                        BeginStoryboard bsb = (BeginStoryboard)action;
                        if (bsb != null && bsb.Storyboard != null)
                        {
                            StopAndRemoveStoryBoard(owner, bsb.Storyboard);
                        }
                    }
                }
                for (int i = actions.Count - 1; i >= 0; i--)
                {
                    actions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Cleanups the event trigger.
        /// </summary>
        /// <param name="trgEvent">The TRG event.</param>
        /// <param name="source">The source.</param>
        public static void CleanupEventTrigger(this EventTrigger trgEvent, FrameworkElement source)
        {
            try
            {
                InvokeMethod(trgEvent, typeof(EventTrigger), "DisconnectOneTrigger", BindingFlags.Static | BindingFlags.NonPublic,
                    new Type[] { typeof(FrameworkElement), typeof(TriggerBase) }, new object[] { source, trgEvent });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Specific Control Methods
        public static void DisposeWPFObjectBase<T>(this T source,
            Action<T> beforeAction,
            Action<DependencyObject> childAction,
            Action<T> afterAction)
            where T : DependencyObject
        {
            source.ClearAllBindings();
            if (beforeAction != null) beforeAction(source);

            source.IterateVisualChild((c) =>
            {
                if (c is AdornerLayer) ((AdornerLayer)c).RemoveAdornerLayerLayoutUpdated();
                else if (childAction != null) childAction(c);
            });

            if (afterAction != null) afterAction(source);
        }

        /// <summary>
        /// Disposes the WPF object base.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObjectBase<T>(this T source, Action<DependencyObject> action)
            where T : DependencyObject
        {
            DisposeWPFObjectBase(source, null, action, null);
        }

        /// <summary>
        /// Disposes the WPF object UI element.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObjectUIElement(this UIElement source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectUIElement(null, action, null);
        }

        /// <summary>
        /// Disposes the WPF object UI element.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObjectUIElement(this UIElement source,
            Action<UIElement> beforeAction,
            Action<DependencyObject> childAction,
            Action<UIElement> afterAction)
        {
            source.DisposeWPFObjectBase(
                   (s) =>
                   {
                       s.RenderClose();
                       if (beforeAction != null) beforeAction(s);
                   },
                   childAction,
                   (s) =>
                   {
                       if (afterAction != null) afterAction(s);
                   });
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this TextBox source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectBase(
                (s) =>
                {

                },
                action,
                (s) =>
                {
                    s.DetachFromVisualTree();
                });
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this PasswordBox source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectBase(
                (s) =>
                {
                    s.InvokeOnThrottleBackgroundTimeout();
                },
                action,
                (s) =>
                {
                    s.DetachFromVisualTree();
                });
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this TextBlock source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectUIElement(action);
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this Shape source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectUIElement(action);
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this ScrollViewer source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectUIElement(action);
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this ScrollBar source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectUIElement(action);
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this Selector source, Action<DependencyObject> action)
        {
            try
            {
                // Items.CurrentChanged
                source.Items.RemoveEventHandler(typeof(CollectionView), BindingFlags.Instance | BindingFlags.Public,
                    "CurrentChanged", source, "OnCurrentChanged");
                // ItemContainerGenerator.OnGeneratorStatusChanged
                source.ItemContainerGenerator.RemoveEventHandler(typeof(ItemContainerGenerator), BindingFlags.Instance | BindingFlags.Public,
                    "StatusChanged", source, "OnGeneratorStatusChanged");

                // KeyboardNavigation.Current.FocusEnterMainFocusScope
                KeyboardNavigation keybdCurrent = source.GetPropertyValue(typeof(KeyboardNavigation), "Current", BindingFlags.Static | BindingFlags.NonPublic,
                    typeof(KeyboardNavigation), null, null) as KeyboardNavigation;
                if (keybdCurrent != null)
                {
                    keybdCurrent.RemoveEventHandler(typeof(KeyboardNavigation), BindingFlags.Instance | BindingFlags.NonPublic,
                        "FocusEnterMainFocusScope", source, "OnFocusEnterMainFocusScope");
                }

                // selectedItems.CollectionChanged
                DependencyProperty selectedItemsImplProperty = source.GetDependencyProperty(typeof(Selector),
                    "SelectedItemsImplProperty", BindingFlags.Static | BindingFlags.NonPublic);
                if (selectedItemsImplProperty != null)
                {
                    ObservableCollection<object> selectedItems =
                        source.GetValue(selectedItemsImplProperty) as ObservableCollection<object>;
                    selectedItems.RemoveEventHandler(typeof(ObservableCollection<>).MakeGenericType(new Type[] { typeof(object) }),
                        BindingFlags.Instance | BindingFlags.Public,
                    "CollectionChanged", source, "OnSelectedItemsCollectionChanged");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            source.DisposeWPFObjectUIElement(action);
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this ButtonBase source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectUIElement(
                (s) => { source.Command = null; },
                action, null);
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this ContentPresenter source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectUIElement(action);
        }

        /// <summary>
        /// Disposes the WPF object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObject(this ContentControl source, Action<DependencyObject> action)
        {
            source.DisposeWPFObjectBase(action);
        }

        /// <summary>
        /// Disposes the WPF object user control.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void DisposeWPFObjectTopControls<T>(this T source, Action<DependencyObject> action)
            where T : ContentControl
        {
            source.ClearVisualTransform();
            source.DisposeWPFObjectBase(action);
        }

        /// <summary>
        /// Cleanups the WPF object top controls.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="intermediateAction">The intermediate action.</param>
        /// <param name="disposeAction">The dispose action.</param>
        public static void CleanupWPFObjectTopControls<T>(this T source, 
            Action<DependencyObject> intermediateAction, 
            Action<DependencyObject> disposeAction)
            where T : ContentControl
        {
            source.ClearAllDependencyProperties();
            if (intermediateAction != null) intermediateAction(source);
            source.DisposeWPFObjectTopControls(disposeAction);
        }
        #endregion

        #region Composite Control Methods

        /// <summary>
        /// Cleanup_s the text box style1.
        /// </summary>
        /// <param name="sources">The sources.</param>
        public static void Cleanup_TextBoxStyle1(params TextBox[] sources)
        {
            foreach (TextBox source in sources)
            {
                source.Cleanup_TextBoxStyle1();
            }
        }

        /// <summary>
        /// Cleanup_s the text box style1.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_TextBoxStyle1(this TextBox source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is Border) ((Border)c).ClearAllBindings();
                if (c is ScrollViewer) ((ScrollViewer)c).Cleanup_TextScrollViewerControlTemplate1();
            });
        }

        /// <summary>
        /// Cleanup_s the login password box style.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_LoginPasswordBoxStyle(this PasswordBox source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is Border) ((Border)c).ClearAllBindings();
                if (c is ScrollViewer) ((ScrollViewer)c).Cleanup_TextScrollViewerControlTemplate1();
            });
        }

        /// <summary>
        /// Cleanup_s the text scroll viewer control template1.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_TextScrollViewerControlTemplate1(this ScrollViewer source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is Rectangle) ((Rectangle)c).DisposeWPFObject(null);
                if (c is ContentPresenter) ((ContentPresenter)c).DisposeWPFObject(null);
                if (c is ScrollContentPresenter) ((ScrollContentPresenter)c).DisposeWPFObject(null);
            });
        }

        /// <summary>
        /// Cleanup_s the floor view_ scroll viewer.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_FloorView_ScrollViewer(this ScrollViewer source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is ScrollBar) ((ScrollBar)c).Cleanup_LeftNav_ScrollBarStyle1();
                if (c is ContentPresenter) ((ContentPresenter)c).DisposeWPFObject(null);
                if (c is Rectangle) ((Rectangle)c).DisposeWPFObject(null);
            });
        }

        /// <summary>
        /// Cleanup_s the left nav_ scroll bar style1.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_LeftNav_ScrollBarStyle1(this ScrollBar source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is RepeatButton) ((RepeatButton)c).Cleanup_ScrollBarButton();
            });
        }

        /// <summary>
        /// Cleanup_s the scroll bar button.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_ScrollBarButton(this RepeatButton source)
        {
            source.DisposeWPFObject((c) =>
            {
            });
        }

        /// <summary>
        /// Cleanup_s the gen_ style_ balloon.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_Gen_Style_Balloon(this Label source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is Rectangle) ((Rectangle)c).DisposeWPFObject(null);
                if (c is Path) ((Path)c).DisposeWPFObject(null);
                if (c is TextBlock) ((TextBlock)c).DisposeWPFObject(null);
            });
        }

        /// <summary>
        /// Cleanup_bmc_s the general_ selected_ state_ button_ style.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Cleanup_bmc_General_Selected_State_Button_Style(this CheckBox source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is Rectangle) ((Rectangle)c).DisposeWPFObject(null);
                if (c is TextBlock) ((TextBlock)c).DisposeWPFObject(null);
            });
        }

        public static void Cleanup_LeftNavPanel(this ListBox source)
        {
            source.DisposeWPFObject((c) =>
            {
                if (c is Border) ((Border)c).DisposeWPFObjectUIElement(null);
                if (c is ScrollContentPresenter) ((ScrollContentPresenter)c).DisposeWPFObject(null);
                if (c is RepeatButton) ((RepeatButton)c).Cleanup_ScrollBarButton();
            });
        }
        #endregion

        #region Log Methods

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="text">The text.</param>
        public static void WriteLog(this FrameworkElement element, string text)
        {
            string name = element.Name;
            string prefix = "|=> ";
            if (!string.IsNullOrEmpty(name)) prefix += "(" + name + ") ";
            LogManager.WriteLog(prefix + text, LogManager.enumLogLevel.Info);
        }

        #endregion        

        #region RemoteObject Connectivity
        public static ICDOMSMQContract InitializeRemoteObject()
        {
            ICDOMSMQContract contract = null;
            string strDefaultServerIP = string.Empty;
     
            try
            {
                //Get Default Server IP address from registry
                strDefaultServerIP = BMCRegistryHelper.GetRegKeyValue("Cashmaster\\Exchange", "Default_Server_IP");

                LogManager.WriteLog("Default Server IP " + strDefaultServerIP, LogManager.enumLogLevel.Info);

                //Establish connection with server 
     
                ICDOMSMQContractFactory instance = Activator.GetObject(typeof(ICDOMSMQContractFactory),
               "tcp://" + strDefaultServerIP + ":9091/CDOMSMQImplementation.rem") as ICDOMSMQContractFactory;
                contract = instance.CreateContract();
                if (contract == null)
                    LogManager.WriteLog("Contract not estabilished", LogManager.enumLogLevel.Info);
                return contract;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
            
        }
        #endregion
    }
    #endregion

    #region EventInfoDelegate Class
    /// <summary>
    /// EventInfoDelegate Class
    /// </summary>
    public class EventInfoDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventInfoDelegate"/> class.
        /// </summary>
        /// <param name="ei">The ei.</param>
        /// <param name="callback">The callback.</param>
        public EventInfoDelegate(EventInfo ei, Delegate callback)
        {
            this.Info = ei;
            this.Callback = callback;
        }

        /// <summary>
        /// Gets or sets the info.
        /// </summary>
        /// <value>The info.</value>
        public EventInfo Info { get; private set; }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>The callback.</value>
        public Delegate Callback { get; private set; }
    }
    #endregion
}
