import { IUser } from '@prommerce/shared-types';
import { createSignal, For, onMount } from 'solid-js';
import styles from './App.module.css';

function App() {
  const [users, setUsers] = createSignal<IUser[]>([]);

  onMount(async () => {
    const data = await fetch('http://localhost:3333/users');
    setUsers(await data.json());
  });

  return (
    <main>
      <h1>Welcome to Prommerce</h1>
      <For each={users()}>{(user) => <p>Name: {user.firstName}</p>}</For>
    </main>
  );
}

export default App;
