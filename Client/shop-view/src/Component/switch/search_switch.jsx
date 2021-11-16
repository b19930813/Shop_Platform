import React from 'react';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';
import CompanySearch from '../company_search/search'
import CustomSearch from '../customs_search/search'
import TableList from '../TableList/TableList'
import axios from 'axios';
import { config } from '../api/config'

export default function Main() {

    const [company, setCompany] = React.useState(false);
    const [custom, setCustom] = React.useState(false);
    const [companyData, setCompanyData] = React.useState([{}]);
    //Call the API to get companies
    React.useEffect(() => {
        axios.get('/api/Companies', config)
            .then(response => {
                setCompanyData(response.data);
            })
    }, [])
    const handleCompanyChange = event => {
        setCompany(!company);
    }

    const handleCustomChange = event => {
        setCustom(!custom)
    }
    return (
        <div>

            <FormGroup row>
                <FormControlLabel
                    control={
                        <Switch
                            checked={company}
                            name="company_switch"
                            color="primary"
                            onChange={handleCompanyChange}
                        />
                    }
                    label="公司篩選"
                />
                <FormControlLabel
                    control={
                        <Switch
                            checked={custom}
                            name="company_switch"
                            color="primary"
                            onChange={handleCustomChange}
                        />
                    }
                    label="客戶篩選"
                />
            </FormGroup>

            <div>
                <CompanySearch props={company} onChange={company} />
                <CustomSearch props={custom} onChange={custom} />
            </div>
            <TableList props={companyData} />
            

        </div>
    );
}