using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace SimpleBlazorGrid.Core.Tests.Blazor.Extensions;

public static class RenderedComponentExtensions
{
    public static List<INode> FetchAllChildNodesRecursively<T>(this IRenderedComponent<T> component) where T : IComponent
    {
        List<INode> AllNodesInList(INodeList nodeList)
        {
            var result = new List<INode>();

            var children = nodeList.SelectMany(x => x.ChildNodes);
            foreach (var child in children)
            {
                result.Add(child);

                if (child.HasChildNodes)
                {
                    result.AddRange(AllNodesInList(child.ChildNodes));
                }
            }

            return result;
        }
        
        
        return AllNodesInList(component.Nodes);
    }
    
    // private static List<INode> GetAllNodesInList(INodeList nodeList)
    // {
    //     List<INode> AllNodesInList()
    //     {
    //         var result = new List<INode>();
    //
    //         var children = nodeList.SelectMany(x => x.ChildNodes);
    //         foreach (var child in children)
    //         {
    //             result.Add(child);
    //
    //             if (child.HasChildNodes)
    //             {
    //                 result.AddRange(GetAllNodesInList(child.ChildNodes));
    //             }
    //         }
    //
    //         return result;
    //     }
    //
    //     return AllNodesInList();
    // }
}