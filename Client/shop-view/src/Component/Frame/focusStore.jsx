import * as React from 'react';
import { makeStyles } from '@material-ui/core';
import axios from 'axios';
import { config } from '../../api/config'
import Store from './Store'
//畫面css

const useStyles = makeStyles(theme => ({
    basic: {
        //paddingTop : 300,
        paddingTop: "20px"
    },
}));

export default function FocusStore() {
    const classes = useStyles();
    return (
        <div className={classes.basic}>
          <Store/>
        </div>
    );
}