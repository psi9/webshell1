import './App.css';
import './components/components.css';

import React, { Fragment } from 'react';
import ShellForm from './components/ShellForm'

class App extends React.Component {
	render() {
		return (
			<Fragment>
				<ShellForm />
			</Fragment>
		);
	}
}
export default App;

