'use client';
// React Grid Logic
import React, { StrictMode, useMemo, useState } from "react";

// Theme
import {
  AllCommunityModule,
  NumberFilterModule,
  DateFilterModule,
  ModuleRegistry,
  ColDef,
  ClientSideRowModelModule,
  ICellRendererParams,
} from "ag-grid-community";
// Core CSS
import { AgGridReact } from "ag-grid-react";

import { ApolloClient, InMemoryCache, gql, useQuery } from '@apollo/client';

ModuleRegistry.registerModules([
  AllCommunityModule,
  NumberFilterModule,
  DateFilterModule,
  ClientSideRowModelModule,
]);

export interface Meeting {
  id: number;
  progress: string;
  time: Date; // ISO-8601 DateTime
  person: string;
  position: string;
  companyAndBoothNumber: string;
  email: string;
  linkedIn: string;
  source: string;
  leadStatus: string;
  afterDiscussionNotes: string;
}

class LinkCellRenderer {
  eGui!: HTMLAnchorElement;

  init(params:ICellRendererParams) {
    this.eGui = document.createElement('a');
    this.eGui.href = params.value;
    this.eGui.innerHTML = params.value;
    this.eGui.target = '_blank';
  }

  // Required: Return the DOM element of the component, this is what the grid puts into the cell
  getGui() {
    return this.eGui;
  }

  // Required: Get the cell to refresh.
  refresh() {
    return false
  }
}

export interface MeetingsData {
  meetings: Meeting[];
}

const GET_MEETINGS = gql`
  query GetMeetings {
    meetings {
      id
      progress
      time
      person
      position
      companyAndBoothNumber
      email
      linkedIn
      source
      leadStatus
      afterDiscussionNotes
    }
  }
`;

const client = new ApolloClient({
  // this is rewritten to use backend API
  uri: `/api/graphql`,
  cache: new InMemoryCache(),
});

const ExcelTable = () => {

  const { loading, error, data } = useQuery<MeetingsData>(GET_MEETINGS, { client });
  // Column Definitions: Defines & controls grid columns.
  const cols:ColDef[] = [
    { field: "id" },
    { field: "progress" },
    { field: "time",
      valueFormatter: params => {
        const dt = new Date(params.value);
        return `${dt.toDateString()} ${dt.toLocaleTimeString()}`
      }
    },
    { field: "person" },
    { field: "position" },
    { field: "companyAndBoothNumber" },
    { field: "email" },
    { field: "linkedIn", cellRenderer: LinkCellRenderer },
    { field: "source" },
    { field: "leadStatus" },
    { field: "afterDiscussionNotes" }
  ];

  const [colDefs] = useState<ColDef[]>(cols);

  // Apply settings across all columns
  const defaultColDef = useMemo<ColDef>(() => {
    return {
      sortable: true,
      filter: true,
    };
  }, []);

  if (loading) return <div>Loading meetings...</div>;
  if (error) return <div>Error fetching meetings: {error.message}</div>;


  // Container: Defines the grid's theme & dimensions.
  return (
    <div style={{ width: "100%", height: "100%" , backgroundColor: "white", color: "black" }}>
      {/* The AG Grid component, with Row Data & Column Definition props */}
      <AgGridReact
        rowData={data?.meetings || []}
        columnDefs={colDefs}
        defaultColDef={defaultColDef}
        pagination={true}
        paginationAutoPageSize={true}
        enableCellTextSelection={true}
        ensureDomOrder={true}
      />
    </div>
  );
};

export default function Home() {
    return (
      <StrictMode>
        <ExcelTable />
      </StrictMode>
    );
}



