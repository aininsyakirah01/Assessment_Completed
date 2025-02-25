import React, { useEffect, useState } from "react";
import axios from "axios";
import "./App.css"; // Ensure this is imported for styling

const API_URL = "http://localhost:5279/api/todos";

const App = () => {
  const [todos, setTodos] = useState([]);
  const [title, setTitle] = useState("");
  const [editingTodo, setEditingTodo] = useState(null);
  const [editTitle, setEditTitle] = useState("");

  // Fetch all todos on component mount
  useEffect(() => {
    axios.get(API_URL)
      .then((res) => setTodos(res.data))
      .catch((error) => console.error("API Error:", error));
  }, []);

  // Add a new todo
  const addTodo = () => {
    if (!title.trim()) return; // Prevent empty input
    axios.post(API_URL, { title, isCompleted: false })
      .then((res) => {
        setTodos([...todos, res.data]);
        setTitle("");
      })
      .catch((error) => console.error("Add Todo Error:", error));
  };

  // Delete a todo
  const deleteTodo = (id) => {
    axios.delete(`${API_URL}/${id}`)
      .then(() => {
        setTodos(todos.filter((todo) => todo.id !== id));
      })
      .catch((error) => console.error("Delete Todo Error:", error));
  };

  // Start updating a todo
  const startUpdating = (todo) => {
    setEditingTodo(todo.id);
    setEditTitle(todo.title);
  };

  // Update a todo
  const updateTodo = (id) => {
    axios.put(`${API_URL}/${id}`, { id, title: editTitle, isCompleted: false })
      .then(() => {
        setTodos(todos.map((todo) =>
          todo.id === id ? { ...todo, title: editTitle } : todo
        ));
        setEditingTodo(null);
        setEditTitle("");
      })
      .catch((error) => console.error("Update Todo Error:", error));
  };

  return (
    <div className="App">
      <h1>To-do List</h1>
      <div>
        <input 
          value={title} 
          onChange={(e) => setTitle(e.target.value)} 
          placeholder="Enter a task..."
        />
        <button className="add-btn" onClick={addTodo}>Add</button>
      </div>
      <ul>
        {todos.map((todo) => (
          <li key={todo.id}>
            {editingTodo === todo.id ? (
              <>
                <input
                  value={editTitle}
                  onChange={(e) => setEditTitle(e.target.value)}
                />
                <button className="save-btn" onClick={() => updateTodo(todo.id)}>Save</button>
                <button className="cancel-btn" onClick={() => setEditingTodo(null)}>Cancel</button>
              </>
            ) : (
              <>
                <span className="todo-text">{todo.title}</span>
                <button className="update-btn" onClick={() => startUpdating(todo)}>Update</button>
                <button className="delete-btn" onClick={() => deleteTodo(todo.id)}>Delete</button>
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};
//try test
export default App;
