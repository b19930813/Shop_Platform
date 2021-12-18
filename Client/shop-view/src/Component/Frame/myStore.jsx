import * as React from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { makeStyles } from '@material-ui/core';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';

const useStyles = makeStyles(theme => ({
  context: {
    paddingTop: "2%",
    paddingLeft: "12%",
    paddingRight: "12%",
  },
  pageCenter: {
    margin: 'auto',
    paddingTop: "2%"
  },
  imageClass: {
    width: '200px',
    height: '200px',
    marginLeft: '50%',
    display: 'flex',
  },
  Text: {

  }
}));

export default function ControlledAccordions() {
  const [expanded, setExpanded] = React.useState(false);
  const classes = useStyles();
  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  const handleAddClick = () =>{
    window.open('/AddCommodity')
  }

  return (
    <div className={classes.context}>
      <div>
          <h1  style={{ 'display': 'inline-block' }}>XXX的商店</h1>
          <Button color="warning" variant="contained" style={{ 'float': 'right' , 'marginTop' : '3%'}} onClick={handleAddClick} >加入商品</Button>
      </div>

        <Accordion expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
          >
            <Typography sx={{ width: '33%', flexShrink: 0 }}>
              滑鼠
            </Typography>
            <Typography sx={{ color: 'text.secondary' }}>價格 : 150</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              <div className={classes.Text}>
                <p>test</p>
              </div>
              <div className={classes.imageClass}>
                <img src="https://localhost:44387/api/User/getImage/1" className={classes.imageClass} />
              </div>

            </Typography>
          </AccordionDetails>
        </Accordion>
        <Accordion expanded={expanded === 'panel2'} onChange={handleChange('panel2')}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
          >
            <Typography sx={{ width: '33%', flexShrink: 0 }}>
              滑鼠
            </Typography>
            <Typography sx={{ color: 'text.secondary' }}>價格 : 150</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              放圖片跟內容
            </Typography>
          </AccordionDetails>
        </Accordion>
        <Accordion expanded={expanded === 'panel3'} onChange={handleChange('panel3')}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
          >
            <Typography sx={{ width: '33%', flexShrink: 0 }}>
              滑鼠
            </Typography>
            <Typography sx={{ color: 'text.secondary' }}>價格 : 150</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              放圖片跟內容
            </Typography>
          </AccordionDetails>
        </Accordion>
        <Accordion expanded={expanded === 'panel4'} onChange={handleChange('panel4')}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
          >
            <Typography sx={{ width: '33%', flexShrink: 0 }}>
              滑鼠
            </Typography>
            <Typography sx={{ color: 'text.secondary' }}>價格 : 150</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              放圖片跟內容
            </Typography>
          </AccordionDetails>
        </Accordion>

      <Stack spacing={2}>
        <Pagination count={10} color="secondary" className={classes.pageCenter} />
      </Stack>
    </div>
  );
}
