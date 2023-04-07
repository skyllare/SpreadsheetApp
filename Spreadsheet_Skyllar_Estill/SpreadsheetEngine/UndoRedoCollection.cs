using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    internal class UndoRedoCollection<T>
    {
        /// <summary>
        /// stack to store undo actions.
        /// </summary>
        private readonly Stack<T> undoStack = new Stack<T>();

        /// <summary>
        /// stack to store redo actions.
        /// </summary>
        private readonly Stack<T> redoStack = new Stack<T>();

        /// <summary>
        /// Gets a value indicating whether tests if the undo stack is empty.
        /// </summary>
        public bool CanUndo => this.undoStack.Count > 0;

        /// <summary>
        /// Gets a value indicating whether checks if the redo stack is empty.
        /// </summary>
        public bool CanRedo => this.redoStack.Count > 0;

        /// <summary>
        /// adds an item to the undo stack and clears the redo stack.
        /// </summary>
        /// <param name="item">item to add to stack</param>
        public void Add(T item)
        {
            this.undoStack.Push(item);
            this.redoStack.Clear();
        }



        /// <summary>
        /// returns the action you want to undo. Pushes the action onto the redo stack.
        /// </summary>
        /// <returns>the actions to undo.</returns>
        /// <exception cref="InvalidOperationException">if the stack is empty.</exception>
        public T Undo()
        {
            if (!this.CanUndo)
            {
                throw new InvalidOperationException("Cannot undo");
            }

            T item = this.undoStack.Pop();
            this.redoStack.Push(item);
            return item;
        }

        /// <summary>
        /// returns the action you want to redo. Pushes the action to undo stack.
        /// </summary>
        /// <returns>the action.</returns>
        /// <exception cref="InvalidOperationException">if the redo stack is empty.</exception>
        public T Redo()
        {
            if (!this.CanRedo)
            {
                throw new InvalidOperationException("Cannot redo");
            }

            T item = this.redoStack.Pop();
            this.undoStack.Push(item);
            return item;
        }

        /// <summary>
        /// clears both stacks.
        /// </summary>
        public void Clear()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
        }
    }

}
