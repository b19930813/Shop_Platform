import React, { Component } from 'react';
import './component.css';
import Bar from './Component/Frame/bar'
import Test from './Component/Frame/test'
import { BrowserRouter, Route, Routes } from "react-router-dom"
import MyStore from './Component/Frame/myStore'
import MyBuyList from './Component/Frame/myBuyList'
import FocusStore from './Component/Frame/focusStore'
import TransHistory from './Component/Frame/transHistory'
import UserInformation from './Component/Frame/userInformation'
import LineBotInformation from './Component/Frame/lineBotInformation'
import Register from './Component/Frame/register'
import Steper from './Component/Tools/Stepper'

//加入Component
class App extends Component {
  render() {
    return (
      <div>
        {/* <Bar/> */}
        <Bar />
        <BrowserRouter>
          <Routes  >
            <Route path="/" element={<Test />} /> 
            <Route path='/MyStore' element={<MyStore/>} />
            <Route path='/MyBuyList' element={<MyBuyList/>} />
            <Route path='/FocusStore' element={<FocusStore/>} />
            <Route path='/TransHistory' element={<TransHistory/>} />
            <Route path='/UserInformation' element={<UserInformation/>} />
            <Route path='/LineBotInformation' element={<LineBotInformation/>} />
            <Route path='/Register' element={<Register/>} />
            <Route path='/Test' element={<Test/>} />
          </Routes  >
        </BrowserRouter>,
      </div>
    )
  }
}

export default App;