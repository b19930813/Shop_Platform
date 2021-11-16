import React, { Component } from 'react';
import './component.css';
 import Bar from './Component/Frame/bar'
import Main from './Component/Frame/test'

//加入Component
class App extends Component {
  render() {
    return (
      <div>
      {/* <Bar/> */}
       <Bar/>
       <Main/>
      </div>
    )
  }
}

export default App;