import { ScheduleType } from "src/data/irrigation/schedule/api-models/ScheduleType";
import { TimeSpan } from "src/data/TimeSpan";

export class ScheduleApiModel {
    id: string;
    name: string;
    description: string;
    scheduleType: ScheduleType;
    startTime: string
    startDate: string;
    duration: string
    enabledUntil: string;
    days: Array<number>;
    zoneIds: Array<string>;
    isEnabled: boolean;

}

export default ScheduleApiModel;