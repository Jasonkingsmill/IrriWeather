﻿import { ScheduleType } from "src/data/irrigation/schedule/api-models/ScheduleType";

export class AddScheduleApiModel {
    name: string;
    description: string;
    scheduleType: ScheduleType;
    startTime: Date;
    startDate: Date;
    duration: Date;
    enabledUntil: Date;
    days: Array<number>;
    zoneIds: Array<string>;
    isEnabled: boolean;
}

export default AddScheduleApiModel;