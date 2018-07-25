import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Zones } from './components/irrigation/zones/Zones';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/irrigation' component={Zones}} />
</Layout>;
